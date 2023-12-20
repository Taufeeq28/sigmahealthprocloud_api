using Data;
using Data.Constant;
using Data.Models;
using Data.Repository;
using Data.RequestModels;
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
    public class CountiesRepository : IGenericRepository<County>, ICountiesRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public CountiesRepository(SigmaproIisContext _context,ILogger<UnitOfWork> logger)
        {
            this.context = _context;
            _logger = logger;
        }
        public async Task<IEnumerable<County>> Find(Expression<Func<County, bool>> predicate)
        {
            return await context.Counties.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<County>> GetAllAsync()
        {
            return await context.Counties.Where(c => c.Isdelete == false).ToListAsync();
        }

        public async Task<County> GetByIdAsync(int id)
        {
            return await context.Set<County>().FindAsync(id);
        }

        public async Task<ApiResponse<string>> InsertAsync(County entity)
        {
            try
            {
                await context.Set<County>().AddAsync(entity);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "County inserted successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Inserting the County.");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(County entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "County Updated successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Updating the County.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await context.Set<County>().FindAsync(id);
                if (entity != null)
                {
                    context.Set<County>().Remove(entity);
                    await context.SaveChangesAsync();
                    return ApiResponse<string>.Success(id.ToString(), "County deleted successfully.");
                }

                return ApiResponse<string>.Fail("County with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("County with the given ID not found.");
            }
        }

        public async Task<List<CountyModel>> GetCountybyStateid(Guid stateid)
        {
            try
            {
                var countylist = new List<CountyModel>();
                var counties = await context.Counties.Where(c => c.StateId.ToString().ToLower().Equals(stateid.ToString().ToLower()) && c.Isdelete == false).ToListAsync();
                foreach (var c in counties)
                {
                    var countymod = new CountyModel()
                    {
                        Id = c.Id,
                        CountyId = c.CountyId,
                        CountyName = c.CountyName,
                        CountyCode = c.CountyCode,
                    };
                    countylist.Add(countymod);
                }

                return countylist;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorelationId: {_corelationId} -Exception occurred in Method: {nameof(GetCountybyStateid)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw new Exception(ex.Message); ;
            }
        }
    }
}
