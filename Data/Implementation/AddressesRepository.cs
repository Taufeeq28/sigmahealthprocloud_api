using Data;
using Data.Models;
using Data.Constant;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Data.Implementation
{
    public class AddressesRepository : IGenericRepository<Address>, IAddressesRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public AddressesRepository(SigmaproIisContext _context,ILogger<UnitOfWork> logger)
        {
            this.context = _context;
            _logger = logger;
        }        

        public async Task<IEnumerable<Address>> Find(Expression<Func<Address, bool>> predicate)
        {
            return await context.Addresses.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await context.Set<Address>().ToListAsync();
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            return await context.Set<Address>().FindAsync(id);
        }

        public async Task<ApiResponse<string>> InsertAsync(Address entity)
        {
            try
            { 
            await context.Set<Address>().AddAsync(entity);
            await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "Address inserted successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Inserting the Address.");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(Address entity)
        {
            try
            { 
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "Address Updated successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Updating the Address.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await context.Set<Address>().FindAsync(id);
                if (entity != null)
                {
                    context.Set<Address>().Remove(entity);
                    await context.SaveChangesAsync();
                    return ApiResponse<string>.Success(id.ToString(), "Address deleted successfully.");
                }

                return ApiResponse<string>.Fail("Address with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("Address with the given ID not found.");
            }
        }

    }
}
