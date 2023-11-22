using NeoSmart.ClassLibraries.Responses;

namespace NeoSmart.BackEnd.Interfaces
{
    public interface IApiService
    {
        Task<Response<T>> GetAsync<T>(string servicePrefix, string controller);
    }
}
