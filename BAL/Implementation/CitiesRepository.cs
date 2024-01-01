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
using BAL.RequestModels;
using BAL.Repository;

namespace BAL.Implementation
{
    public class CitiesRepository : IGenericRepository<City>, ICitiesRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public CitiesRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger)
        {
            context = _context;
            _logger = logger;
        }

        public async Task<IEnumerable<City>> Find(Expression<Func<City, bool>> predicate)
        {
            return await context.Cities.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await context.Set<City>().ToListAsync();
        }

        public async Task<City> GetByIdAsync(int id)
        {
            return await context.Set<City>().FindAsync(id);
        }

        public async Task<ApiResponse<string>> InsertAsync(City entity)
        {
            try
            {
                await context.Set<City>().AddAsync(entity);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "City inserted successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Inserting the City.");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(City entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "City Updated successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Updating the City.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await context.Set<City>().FindAsync(id);
                if (entity != null)
                {
                    context.Set<City>().Remove(entity);
                    await context.SaveChangesAsync();
                    return ApiResponse<string>.Success(id.ToString(), "City deleted successfully.");
                }

                return ApiResponse<string>.Fail("City with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("City with the given ID not found.");
            }
        }
        public async Task<List<CityModel>> GetCitybyStateid(Guid stateid)
        {
            try
            {
                var citylist = new List<CityModel>();
                var cities = await context.Cities.Where(c => c.StateId.ToString().ToLower().Equals(stateid.ToString().ToLower()) && c.Isdelete == false).ToListAsync();
                foreach (var c in cities)
                {
                    var citymod = new CityModel()
                    {
                        Id = c.Id,
                        CityId = c.CityId,
                        CityName = c.CityName

                    };
                    citylist.Add(citymod);
                }

                return citylist;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorelationId: {_corelationId} -Exception occurred in Method: {nameof(GetCitybyStateid)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw new Exception(ex.Message); ;
            }
        }
        public async Task<List<CityModel>> GetCitybyStateidandCountyid(Guid stateid, Guid countyid)
        {
            try
            {
                var citylist = new List<CityModel>();
                var cities = await context.Cities.Where(c => c.StateId.ToString().ToLower().Equals(stateid.ToString().ToLower()) &&
                c.CountyId.ToString().ToLower().Equals(countyid.ToString().ToLower()) &&
                c.Isdelete == false).ToListAsync();
                foreach (var c in cities)
                {
                    var citymod = new CityModel()
                    {
                        Id = c.Id,
                        CityId = c.CityId,
                        CityName = c.CityName

                    };
                    citylist.Add(citymod);
                }

                return citylist;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorelationId: {_corelationId} -Exception occurred in Method: {nameof(GetCitybyStateidandCountyid)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw new Exception(ex.Message); ;
            }
        }
    }
}
