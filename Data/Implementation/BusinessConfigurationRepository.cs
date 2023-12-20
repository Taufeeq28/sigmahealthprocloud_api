using Data.Constant;
using Data.Models;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class BusinessConfigurationRepository : IGenericRepository<BusinessConfiguration>, IBusinessConfigurationRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public BusinessConfigurationRepository(SigmaproIisContext _context,ILogger<UnitOfWork> logger)
        {
            this.context = _context;
            _logger = logger;
        }

        public async Task<IEnumerable<BusinessConfiguration>> Find(Expression<Func<BusinessConfiguration, bool>> predicate)
        {
            return await context.BusinessConfigurations.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<BusinessConfiguration>> GetAllAsync()
        {
            return await context.Set<BusinessConfiguration>().ToListAsync();
        }

        public async Task<BusinessConfiguration> GetByIdAsync(int id)
        {
            return await context.Set<BusinessConfiguration>().FindAsync(id);
        }

        public async Task<ApiResponse<string>> InsertAsync(BusinessConfiguration entity)
        {
            try
            {
                await context.Set<BusinessConfiguration>().AddAsync(entity);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "BusinessConfiguration inserted successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Inserting the BusinessConfiguration.");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(BusinessConfiguration entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "BusinessConfiguration Updated successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Updating the BusinessConfiguration.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await context.Set<BusinessConfiguration>().FindAsync(id);
                if (entity != null)
                {
                    context.Set<BusinessConfiguration>().Remove(entity);
                    await context.SaveChangesAsync();
                    return ApiResponse<string>.Success(id.ToString(), "BusinessConfiguration deleted successfully.");
                }

                return ApiResponse<string>.Fail("BusinessConfiguration with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("BusinessConfiguration with the given ID not found.");
            }
        }

    }
}
