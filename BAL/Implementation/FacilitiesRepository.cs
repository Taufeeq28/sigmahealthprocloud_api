using BAL.Repository;
using BAL.Constant;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Implementation
{
    public class FacilitiesRepository : IGenericRepository<Facility>, IFacilitiesRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public FacilitiesRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger)
        {
            context = _context;
            _logger = logger;
        }

        public async Task<IEnumerable<Facility>> Find(Expression<Func<Facility, bool>> predicate)
        {
            return await context.Facilities.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Facility>> GetAllAsync()
        {
            return await context.Set<Facility>().ToListAsync();
        }

        public async Task<Facility> GetByIdAsync(int id)
        {
            return await context.Set<Facility>().FindAsync(id);
        }

        public async Task<ApiResponse<string>> InsertAsync(Facility entity)
        {
            try
            {
                await context.Set<Facility>().AddAsync(entity);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "Facility inserted successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Inserting the Facility.");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(Facility entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "Facility Updated successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Updating the Facility.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await context.Set<Facility>().FindAsync(id);
                if (entity != null)
                {
                    context.Set<Facility>().Remove(entity);
                    await context.SaveChangesAsync();
                    return ApiResponse<string>.Success(id.ToString(), "Facility deleted successfully.");
                }

                return ApiResponse<string>.Fail("Facility with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("Facility with the given ID not found.");
            }
        }
    }
}
