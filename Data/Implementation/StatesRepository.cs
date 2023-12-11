using Data;
using Data.Models;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class StatesRepository : IGenericRepository<State>, IStatesRepository
    {
        //private SigmaproIisContext context;
        private SigmaproIisContext context;
        public StatesRepository(SigmaproIisContext _context) 
        {

            this.context = _context;
        }

        public void Add(State entity)
        {
            context.States.Add(entity);
        }

        public void AddRange(IEnumerable<State> entities)
        {
            context.States.AddRange(entities);
        }

        public IEnumerable<State> Find(Expression<Func<State, bool>> predicate)
        {
              return context.States.Where(predicate);
            
        }

        public IEnumerable<State> GetAll()
        {
            return context.States.ToList();
        }

        public State? GetById(int id)
        {
            return (State?)context.States.Find(id);
        }

        public void Remove(State entity)
        {
            context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<State> entities)
        {
            context.RemoveRange(entities);
        }
    }
}
