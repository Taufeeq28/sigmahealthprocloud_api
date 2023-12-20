using Data;
using Data.Constant;
using Data.Models;
using Data.Repository;
using Data.RequestModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class StatesRepository : IGenericRepository<State>, IStatesRepository
    {
        //private SigmaproIisContext context;
        private SigmaproIisContext context;
        private readonly ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public StatesRepository(SigmaproIisContext _context,ILogger<UnitOfWork> logger) 
        {

            this.context = _context;
            _logger = logger;
        }

        public async Task<IEnumerable<State>> Find(Expression<Func<State, bool>> predicate)
        {
            return await context.States.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<State>> GetAllAsync()
        {
            return await context.States.Where(s => s.Isdelete == false).ToListAsync();
        }

        public async Task<State> GetByIdAsync(int id)
        {
            return await context.Set<State>().FindAsync(id);
        }

        public async Task<ApiResponse<string>> InsertAsync(State entity)
        {
            try
            {
                await context.Set<State>().AddAsync(entity);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "State inserted successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Inserting the State.");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(State entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "State Updated successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Updating the State.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await context.Set<State>().FindAsync(id);
                if (entity != null)
                {
                    context.Set<State>().Remove(entity);
                    await context.SaveChangesAsync();
                    return ApiResponse<string>.Success(id.ToString(), "State deleted successfully.");
                }

                return ApiResponse<string>.Fail("State with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("State with the given ID not found.");
            }
        }
        public async Task<List<StateModel>> GetStatebyCountryid(Guid countryid)
        {
            try
            { 
                var statelist=new List<StateModel>();
                var state= await context.States.Where(s => s.CountryId.ToString().ToLower().Equals(countryid.ToString().ToLower()) && s.Isdelete == false).ToListAsync();
                foreach (var s in state) 
                {
                    var statemod = new StateModel()
                    {
                        Id = s.Id,
                        StateId = s.StateId,
                        StateName = s.StateName,
                        StateCode = s.StateCode,
                    };
                    statelist.Add(statemod);
                }
                               
                return statelist;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorelationId: {_corelationId} -Exception occurred in Method: {nameof(GetStatebyCountryid)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw new Exception(ex.Message); ;
            }
        }
        public async Task<List<StateModel>> GetAllStates()
        {
            try
            {
                var statelist = new List<StateModel>();
                var state = await context.States.Where(s => s.Isdelete == false).ToListAsync();
                foreach (var s in state)
                {
                    var statemod = new StateModel()
                    {
                        Id = s.Id,
                        StateId = s.StateId,
                        StateName = s.StateName,
                        StateCode = s.StateCode,
                    };
                    statelist.Add(statemod);
                }

                return statelist;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorelationId: {_corelationId} -Exception occurred in Method: {nameof(GetAllStates)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw new Exception(ex.Message); ;
            }
        }
    }
}
