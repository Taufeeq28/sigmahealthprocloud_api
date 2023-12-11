using Data.Models;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class BusinessConfigurationRepository : IGenericRepository<BusinessConfiguration>, IBusinessConfigurationRepository
    {
        private SigmaproIisContext context;
        public BusinessConfigurationRepository(SigmaproIisContext _context)
        {
            this.context = _context;
        }

        public void Add(BusinessConfiguration entity)
        {
            context.BusinessConfigurations.Add(entity);
        }

        public void AddRange(IEnumerable<BusinessConfiguration> entities)
        {
            context.BusinessConfigurations.AddRange(entities);
        }

        public IEnumerable<BusinessConfiguration> Find(Expression<Func<BusinessConfiguration, bool>> predicate)
        {
            return context.BusinessConfigurations.Where(predicate);
        }

        public IEnumerable<BusinessConfiguration> GetAll()
        {
            return context.BusinessConfigurations.ToList();
        }

        public BusinessConfiguration? GetById(int id)
        {
            return (BusinessConfiguration?)context.BusinessConfigurations.Find(id);
        }

        public void Remove(BusinessConfiguration entity)
        {
            context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<BusinessConfiguration> entities)
        {
            context.RemoveRange(entities);
        }
    }
}
