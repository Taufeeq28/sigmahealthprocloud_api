using Data.Models;
using Data.Repository;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Linq.Expressions;


namespace Data.Implementation
{
    public class JuridictionsRepository : IGenericRepository<Juridiction>, IJuridictionsRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        public JuridictionsRepository(SigmaproIisContext _context,ILogger<UnitOfWork> logger) 
        {
            this.context = _context;
            _logger = logger;
        }

        public void Add(Juridiction entity)
        {
            try
            {
                context.Juridictions.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(Add)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }


        public void AddRange(IEnumerable<Juridiction> entities)
        {
            try
            {
                context.Juridictions.AddRange(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(AddRange)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public IEnumerable<Juridiction> Find(Expression<Func<Juridiction, bool>> predicate)
        {
            try
            {
                return context.Juridictions.Where(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(Find)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public IEnumerable<Juridiction> GetAll()
        {
            try
            {
                return context.Juridictions.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(GetAll)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public Juridiction? GetById(int id)
        {
            try
            {
                return (Juridiction?)context.Juridictions.Find(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(GetById)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public IEnumerable<Juridiction> GetJuridictionsbyBusinessid(string businessid)
        {
            try
            {
                var jurdictionmodel = context.Juridictions.Where(j => j.AlternateId.ToString().Equals(businessid));
                if (jurdictionmodel != null)
                {
                    return jurdictionmodel;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(GetJuridictionsbyBusinessid)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public void Remove(Juridiction entity)
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

        public void RemoveRange(IEnumerable<Juridiction> entities)
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
