using Data;
using Data.Models;
using Data.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class LovTypeMasterRepository : IGenericRepository<LovMaster>, ILOVTypeMasterRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        public LovTypeMasterRepository(SigmaproIisContext _context,ILogger<UnitOfWork> logger) 
        {
            this.context = _context;
            _logger = logger;
        }
        public void Add(LovMaster entity)
        {
            try
            {
                context.LovMasters.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(Add)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public void AddRange(IEnumerable<LovMaster> entities)
        {
            try
            {
                context.LovMasters.AddRange(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(AddRange)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public IEnumerable<LovMaster> Find(Expression<Func<LovMaster, bool>> predicate)
        {
            try
            {
                return context.LovMasters.Where(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(Find)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public IEnumerable<LovMaster> GetAll()
        {
            try
            {
                return context.LovMasters.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(GetAll)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public LovMaster? GetById(int id)
        {
            try
            {
                return (LovMaster?)context.LovMasters.Find(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(GetById)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public void Remove(LovMaster entity)
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

        public void RemoveRange(IEnumerable<LovMaster> entities)
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
