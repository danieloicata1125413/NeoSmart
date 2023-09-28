using Microsoft.EntityFrameworkCore;
using NeoSmart.Backend.Intertfaces;
using NeoSmart.ClassLibraries.Models;
using NeoSmart.Data.Entities;

namespace NeoSmart.Backend.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _entity;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }

        public async Task<Response<T>> AddAsync(T entity)
        {
            _context.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return new Response<T>
                {
                    IsSuccess = true,
                    Result = entity
                };
            }
            catch (DbUpdateException dbUpdateException)
            {
                return DbUpdateExceptionResponse(dbUpdateException);
            }
            catch (Exception exception)
            {
                return ExceptionResponse(exception);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            if (row != null)
            {
                _entity.Remove(row);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T> GetAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            return row!;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _entity.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(int skip, int take)
        {
            return await _entity.Skip(skip * take).Take(take).ToListAsync();
        }

        public async Task<Response<T>> UpdateAsync(T entity)
        {
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return new Response<T>
                {
                    IsSuccess = true,
                    Result = entity
                };
            }
            catch (DbUpdateException dbUpdateException)
            {
                return DbUpdateExceptionResponse(dbUpdateException);
            }
            catch (Exception exception)
            {
                return ExceptionResponse(exception);
            }
        }

        private Response<T> ExceptionResponse(Exception exception)
        {
            return new Response<T>
            {
                IsSuccess = false,
                Message = exception.Message
            };
        }

        private Response<T> DbUpdateExceptionResponse(DbUpdateException dbUpdateException)
        {
            if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
            {
                return new Response<T>
                {
                    IsSuccess = false,
                    Message = "Ya existe el registro que estas intentando crear."
                };
            }
            else
            {
                return new Response<T>
                {
                    IsSuccess = false,
                    Message = dbUpdateException.InnerException.Message
                };
            }
        }

       
    }
}
