using Data.Models;using Data;
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

namespace Data.Implementation
{
    public class CountriesRepository : IGenericRepository<Country>, ICountriesRepository
    {
        private SigmaproIisContext context;
        private readonly ILogger<UnitOfWork> _logger;
        public CountriesRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger)
        {
            this.context = _context;
            _logger = logger;
        }

        public void Add(Country entity)
        {
            try
            {
                context.Countries.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(Add)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public void AddRange(IEnumerable<Country> entities)
        {
            try
            {
                context.Countries.AddRange(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(AddRange)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public IEnumerable<Country> Find(Expression<Func<Country, bool>> predicate)
        {
            try
            {
                return context.Countries.Where(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(Find)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public IEnumerable<Country> GetAll()
        {
            try
            {
                return context.Countries.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(GetAll)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }

        }

        public Country? GetById(int id)
        {
            try
            {
                return (Country?)context.Countries.Find(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(GetById)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }

        }

        public void Remove(Country entity)
        {
            try
            {
                context.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(Remove)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public void RemoveRange(IEnumerable<Country> entities)
        {
            try
            {
                context.RemoveRange(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(RemoveRange)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }
    }
}
