using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Responses;

namespace NeoSmart.BackEnd.Intertfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAsync();
        Task<IEnumerable<T>> GetAsync(int skip, int take);
        Task<Response<T>> AddAsync(T entity);
        Task DeleteAsync(int id);
        Task<Response<T>> UpdateAsync(T entity);
        Task<Country> GetCountryAsync(int id);
        Task<State> GetStateAsync(int id);
    }
}
