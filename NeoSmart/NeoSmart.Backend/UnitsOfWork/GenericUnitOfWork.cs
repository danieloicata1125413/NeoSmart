using NeoSmart.Backend.Intertfaces;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Models;

namespace NeoSmart.Backend.UnitsOfWork
{
    public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericUnitOfWork(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<Response<T>> AddAsync(T model) => await _repository.AddAsync(model);
        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
        public async Task<IEnumerable<T>> GetAsync() => await _repository.GetAsync();
        public async Task<IEnumerable<T>> GetAsync(int skip, int take) => await _repository.GetAsync(skip,take);
        public async Task<T> GetAsync(int id) => await _repository.GetAsync(id);
        public async Task<Response<T>> UpdateAsync(T model) => await _repository.UpdateAsync(model);
        public async Task<Country> GetCountryAsync(int id) => await _repository.GetCountryAsync(id);
    }
}