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
    public class FacilitiesRepository : IGenericRepository<Facility>, IFacilitiesRepository
    {
        private SigmaproIisContext context;
        public FacilitiesRepository(SigmaproIisContext _context)
        {
            this.context = _context;
        }

        public void Add(Facility entity)
        {
            context.Facilities.Add(entity);
        }

        public void AddRange(IEnumerable<Facility> entities)
        {
            context.Facilities.AddRange(entities);
        }

        public IEnumerable<Facility> Find(Expression<Func<Facility, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Facility> GetAll()
        {
            return context.Facilities.ToList();
        }

        public Facility? GetById(int id)
        {
            return (Facility?)context.Facilities.Find(id);
        }

        public void Remove(Facility entity)
        {
            context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Facility> entities)
        {
            context.RemoveRange(entities);
        }
    }
}
