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

namespace Data.Implementation
{
    public class CountiesRepository : IGenericRepository<County>, ICountiesRepository
    {
        private SigmaproIisContext context;
        public CountiesRepository(SigmaproIisContext _context)
        {
            this.context = _context;
        }

        public void Add(County entity)
        {
            context.Counties.Add(entity);
        }

        public void AddRange(IEnumerable<County> entities)
        {
            context.Counties.AddRange(entities);
        }

        public IEnumerable<County> Find(Expression<Func<County, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<County> GetAll()
        {
            return context.Counties.ToList();
        }

        public County? GetById(int id)
        {
            return (County?)context.Counties.Find(id);
        }

        public void Remove(County entity)
        {
            context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<County> entities)
        {
            context.RemoveRange(entities);
        }
    }
}
