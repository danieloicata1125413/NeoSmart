using MailKit.Net.Smtp;
using MimeKit;
using NeoSmart.ClassLibraries.Models;
using System.Security.Authentication;
namespace NeoSmart.Backend.Helper
{
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;

        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Response<string> SendMail(string toName, string toEmail, string subject, string body)
        {
            try
            {
                var from = _configuration["Mail:From"];
                var name = _configuration["Mail:Name"];
                var smtp = _configuration["Mail:Smtp"];
                var port = _configuration["Mail:Port"];
                var key = _configuration["Mail:Key"];
                var password = _configuration["Mail:Password"];

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(name, from));
                message.To.Add(new MailboxAddress(toName, toEmail));
                message.Subject = subject;
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = body
                };
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(smtp, int.Parse(port!), true);
                    client.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
                    client.Authenticate(from, key);
                    client.Send(message);
                    client.Disconnect(true);
                }

                return new Response<string> { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
