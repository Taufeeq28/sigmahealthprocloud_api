using Data;
using Data.Models;
using Data.Repository;
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
    public class OrganizationsRepository : IGenericRepository<Organization>, IOrganizationsRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        public OrganizationsRepository(SigmaproIisContext _context,ILogger<UnitOfWork> logger) 
        {
            this.context = _context;
            _logger = logger;
        }

        public void Add(Organization entity)
        {
            try
            {
                context.Organizations.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(Add)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public void AddRange(IEnumerable<Organization> entities)
        {
            try
            {
                context.Organizations.AddRange(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(AddRange)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public IEnumerable<Organization> Find(Expression<Func<Organization, bool>> predicate)
        {
            try
            {
                return context.Organizations.Where(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(Find)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public IEnumerable<Organization> GetAll()
        {
            try
            {
                return context.Organizations.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(GetAll)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public Organization? GetById(int id)
        {
            try
            {
                return (Organization?)context.Organizations.Find(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(GetById)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }
        public IEnumerable<Organization> GetOrganizationByJuridictionId(string jurdid)
        {
            try
            {
                var orgmodel = context.Organizations.Where(o => o.JuridictionId.ToString().Equals(jurdid));
                if (orgmodel != null)
                {
                    return orgmodel;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(GetOrganizationByJuridictionId)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public void Remove(Organization entity)
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

        public void RemoveRange(IEnumerable<Organization> entities)
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
