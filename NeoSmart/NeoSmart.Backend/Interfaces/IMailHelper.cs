using NeoSmart.ClassLibraries.Responses;

namespace NeoSmart.BackEnd.Helper
{
    public interface IMailHelper
    {
        Response<string> SendMail(string toName, string toEmail, string subject, string body);
    }
}
