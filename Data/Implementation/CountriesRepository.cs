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
    public class CountriesRepository : IGenericRepository<Country>, ICountriesRepository
    {
        private SigmaproIisContext context;
        public CountriesRepository(SigmaproIisContext _context)
        {
            this.context = _context;
        }

        public void Add(Country entity)
        {
            context.Countries.Add(entity);
        }

        public void AddRange(IEnumerable<Country> entities)
        {
            context.Countries.AddRange(entities);
        }

        public IEnumerable<Country> Find(Expression<Func<Country, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Country> GetAll()
        {
            return context.Countries.ToList();
        }

        public Country? GetById(int id)
        {
            return (Country?)context.Countries.Find(id);
        }

        public void Remove(Country entity)
        {
            context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Country> entities)
        {
            context.RemoveRange(entities);
        }
    }
}
