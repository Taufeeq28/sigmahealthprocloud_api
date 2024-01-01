using BAL.Repository;
using BAL.RequestModels;
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
    public class LovTypeMasterRepository : IGenericRepository<LovMaster>, ILOVTypeMasterRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public LovTypeMasterRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger)
        {
            context = _context;
            _logger = logger;
        }
        public async Task<IEnumerable<LovMaster>> Find(Expression<Func<LovMaster, bool>> predicate)
        {
            return await context.LovMasters.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<LovMaster>> GetAllAsync()
        {
            return await context.Set<LovMaster>().ToListAsync();
        }

        public async Task<LovMaster> GetByIdAsync(int id)
        {
            return await context.Set<LovMaster>().FindAsync(id);
        }

        public async Task<ApiResponse<string>> InsertAsync(LovMaster entity)
        {
            try
            {
                await context.Set<LovMaster>().AddAsync(entity);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "LovMaster inserted successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Inserting the LovMaster.");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(LovMaster entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "LovMaster Updated successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Updating the LovMaster.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await context.Set<LovMaster>().FindAsync(id);
                if (entity != null)
                {
                    context.Set<LovMaster>().Remove(entity);
                    await context.SaveChangesAsync();
                    return ApiResponse<string>.Success(id.ToString(), "LovMaster deleted successfully.");
                }

                return ApiResponse<string>.Fail("LovMaster with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("LovMaster with the given ID not found.");
            }
        }

        public async Task<List<LOVMasterModel>> GetLOVMasterbyLOVTypeid(string lovtype)
        {
            try
            {
                var lovmasterlist = new List<LOVMasterModel>();
                var lovmaster = await context.LovMasters.Where(s => s.LovType.Equals(lovtype) && s.Isdelete == false).ToListAsync();
                foreach (var l in lovmaster)
                {
                    var lovmastermod = new LOVMasterModel()
                    {
                        Id = l.Id,
                        Key = l.Key,
                        Value = l.Value,
                        LovType = l.LovType,
                        LongDescription = l.LongDescription

                    };
                    lovmasterlist.Add(lovmastermod);
                }

                return lovmasterlist;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorelationId: {_corelationId} -Exception occurred in Method: {nameof(GetLOVMasterbyLOVTypeid)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw new Exception(ex.Message); ;
            }
        }
    }

}
