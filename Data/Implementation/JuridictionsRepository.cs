using Data.Constant;
using Data.Models;
using Data.Repository;
using Data.RequestModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Linq.Expressions;


namespace Data.Implementation
{
    public class JuridictionsRepository : IGenericRepository<Juridiction>, IJuridictionsRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public JuridictionsRepository(SigmaproIisContext _context,ILogger<UnitOfWork> logger) 
        {
            this.context = _context;
            _logger = logger;
        }

        public async Task<IEnumerable<Juridiction>> Find(Expression<Func<Juridiction, bool>> predicate)
        {
            return await context.Juridictions.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Juridiction>> GetAllAsync()
        {
            return await context.Juridictions.Where(j=>j.Isdelete==false).ToListAsync();
        }

        public async Task<Juridiction> GetByIdAsync(int id)
        {
            return await context.Set<Juridiction>().FindAsync(id);
        }

        public async Task<ApiResponse<string>> InsertAsync(Juridiction entity)
        {
            try
            {
                await context.Set<Juridiction>().AddAsync(entity);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "Juridiction inserted successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Inserting the Juridiction.");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(Juridiction entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "Juridiction Updated successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Updating the Juridiction.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await context.Set<Juridiction>().FindAsync(id);
                if (entity != null)
                {
                    context.Set<Juridiction>().Remove(entity);
                    await context.SaveChangesAsync();
                    return ApiResponse<string>.Success(id.ToString(), "Juridiction deleted successfully.");
                }

                return ApiResponse<string>.Fail("Juridiction with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("Juridiction with the given ID not found.");
            }
        }

        public async Task<List<JuridictionModel>> GetJuridictionsbyBusinessid(Guid businessid)
        {
            try
            {
                var jurdlist = new List<JuridictionModel>();
                var jurdiction = await context.Juridictions.Where(j => j.AlternateId.ToString().ToLower().Equals(businessid.ToString().ToLower()) && j.Isdelete == false).ToListAsync();
                foreach (var entity in jurdiction)
                {
                    var jurdictionmod = new JuridictionModel()
                    {
                        JuridictionId=entity.JuridictionId,
                        JuridictionName=entity.JuridictionName,
                        StateId=entity.StateId,
                        AlternateId=entity.AlternateId

                    };
                    jurdlist.Add(jurdictionmod);
                }
                return jurdlist;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorelationId: {_corelationId} -Exception occurred in Method: {nameof(GetJuridictionsbyBusinessid)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw new Exception(ex.Message);
            }
        }

        


    }
}
