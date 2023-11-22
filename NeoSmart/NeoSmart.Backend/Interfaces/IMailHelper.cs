using NeoSmart.ClassLibraries.Responses;

namespace NeoSmart.BackEnd.Interfaces
{
    public interface IMailHelper
    {
        Response<string> SendMail(string toName, string toEmail, string subject, string body);
    }
}
