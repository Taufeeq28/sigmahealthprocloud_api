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
    public class LovTypeMasterRepository : IGenericRepository<LovMaster>, ILOVTypeMasterRepository
    {
        private SigmaproIisContext context;
        public LovTypeMasterRepository(SigmaproIisContext _context) 
        {
            this.context = _context;
        }
        public void Add(LovMaster entity)
        {
            context.LovMasters.Add(entity);
        }

        public void AddRange(IEnumerable<LovMaster> entities)
        {
            context.LovMasters.AddRange(entities);
        }

        public IEnumerable<LovMaster> Find(Expression<Func<LovMaster, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LovMaster> GetAll()
        {
            return context.LovMasters.ToList();
        }

        public LovMaster? GetById(int id)
        {
            return (LovMaster?)context.LovMasters.Find(id);
        }

        public void Remove(LovMaster entity)
        {
            context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<LovMaster> entities)
        {
            context.RemoveRange(entities);
        }

    }
  
}
