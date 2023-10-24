using NeoSmart.ClassLibraries.Models;

namespace NeoSmart.Backend.Helper
{
    public interface IMailHelper
    {
        Response<string> SendMail(string toName, string toEmail, string subject, string body);
    }
}
