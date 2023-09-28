using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using NeoSmart.ClassLibraries.Models;
using System.Security.Authentication;

namespace NeoSmart.ClassLibraries.Helper
{
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;

        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Response<bool> SendMail(string to, string subject, string body)
        {
            try
            {
                string from = _configuration["Mail:From"];
                string smtp = _configuration["Mail:Smtp"];
                string port = _configuration["Mail:Port"];
                string password = _configuration["Mail:Password"];

                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("", from));
                message.To.Add(new MailboxAddress("", to));
                message.Subject = subject;
                BodyBuilder bodyBuilder = new BodyBuilder
                {
                    HtmlBody = body
                };
                message.Body = bodyBuilder.ToMessageBody();

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect(smtp, int.Parse(port), true);
                    client.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
                    client.Authenticate(from, password);
                    client.Send(message);
                    client.Disconnect(true);
                }

                return new Response<bool> { IsSuccess = true };

            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = false
                };
            }
        }
    }
}
