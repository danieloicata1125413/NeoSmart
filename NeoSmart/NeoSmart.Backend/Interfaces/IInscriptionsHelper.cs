using NeoSmart.ClassLibraries.Responses;

namespace NeoSmart.BackEnd.Interfaces
{
    public interface IInscriptionsHelper
    {
        Task<Response<bool>> ProcessInscriptionAsync(string email);
    }
}
