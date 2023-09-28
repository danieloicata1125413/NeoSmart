using NeoSmart.ClassLibraries.Models;

namespace NeoSmart.ClassLibraries.Helper
{
    public interface IMailHelper
    {
        Response<bool> SendMail(string to, string subject, string body);
    }
}
