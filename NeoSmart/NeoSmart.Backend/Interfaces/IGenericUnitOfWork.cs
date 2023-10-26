using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Responses;

namespace NeoSmart.BackEnd.Intertfaces
{
    public interface IGenericUnitOfWork<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();

        Task<Response<T>> AddAsync(T model);

        Task<Response<T>> UpdateAsync(T model);

        Task DeleteAsync(int id);

        Task<T> GetAsync(int id);

        Task<Country> GetCountryAsync(int id);
        Task<State> GetStateAsync(int id);
    }
}
