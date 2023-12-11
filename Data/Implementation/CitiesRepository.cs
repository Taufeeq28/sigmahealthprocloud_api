using Data;
using Data.Models;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class CitiesRepository : IGenericRepository<City>, ICitiesRepository
    {
        private SigmaproIisContext context;
        public CitiesRepository(SigmaproIisContext _context)
        {
            this.context = _context;
        }

        public void Add(City entity)
        {
            context.Cities.Add(entity);
        }

        public void AddRange(IEnumerable<City> entities)
        {
            context.Cities.AddRange(entities);
        }

        public IEnumerable<City> Find(Expression<Func<City, bool>> predicate)
        {
            return context.Cities.Where(predicate);
        }

        public IEnumerable<City> GetAll()
        {
            return context.Cities.ToList();
        }

        public City? GetById(int id)
        {
            return (City?)context.Cities.Find(id);
        }

        public void Remove(City entity)
        {
            context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<City> entities)
        {
            context.RemoveRange(entities);
        }
    }
}
