using Data.Constant;
using Data.Models;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SigmaproIisContext _context;
        public GenericRepository(SigmaproIisContext context)
        {
            _context = context;
        }
       
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            
            return await _context.Set<T>().FindAsync(id);
        }
        public T? GetByuserId(string? user_id)
        {

            return _context.Set<T>().Find(user_id);
        }
        public async Task<ApiResponse<string>> InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return ApiResponse<string>.Success(null, "entity inserted successfully.");
        }


        public async Task<ApiResponse<string>> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ApiResponse<string>.Success(null, "entity updated successfully.");
        }

        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();                
            }
            return ApiResponse<string>.Success(null, "entity removed successfully.");
        }
    }
}
