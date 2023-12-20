using Data.Models;
using Data;
using Data.Models;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Data.Constant;
using Data.RequestModels;

namespace Data.Implementation
{
    public class CountriesRepository : IGenericRepository<Country>, ICountriesRepository
    {
        private SigmaproIisContext context;
        private readonly ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public CountriesRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger)
        {
            this.context = _context;
            _logger = logger;
        }
        public async Task<IEnumerable<Country>> Find(Expression<Func<Country, bool>> predicate)
        {
            return await context.Countries.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await context.Countries.Where(c => c.Isdelete == false).ToListAsync();
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            return await context.Set<Country>().FindAsync(id);
        }

        public async Task<ApiResponse<string>> InsertAsync(Country entity)
        {
            try
            {
                await context.Set<Country>().AddAsync(entity);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "Country inserted successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Inserting the Country.");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(Country entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "Country Updated successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Updating the Country.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await context.Set<Country>().FindAsync(id);
                if (entity != null)
                {
                    context.Set<Country>().Remove(entity);
                    await context.SaveChangesAsync();
                    return ApiResponse<string>.Success(id.ToString(), "Country deleted successfully.");
                }

                return ApiResponse<string>.Fail("Country with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("Country with the given ID not found.");
            }
        }

        public async Task<List<CountryModel>> GetAllCountries()
        {
            try
            {
                var countrylist = new List<CountryModel>();
                var country = await context.Countries.Where(c => c.Isdelete == false).ToListAsync();
                foreach (var c in country)
                {
                    var countrymod = new CountryModel()
                    {
                       Id= c.Id,
                       CountryId = c.CountryId,
                       CountryName = c.CountryName,
                       Alpha2code= c.Alpha2code,
                       Alpha3code= c.Alpha3code,
                    };
                    countrylist.Add(countrymod);
                }

                return countrylist;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorelationId: {_corelationId} -Exception occurred in Method: {nameof(GetAllCountries)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw new Exception(ex.Message); ;
            }
        }
    }
    }
