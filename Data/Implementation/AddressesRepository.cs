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
    public class AddressesRepository : IGenericRepository<Address>, IAddressesRepository
    {
        private SigmaproIisContext context;
        public AddressesRepository(SigmaproIisContext _context)
        {
            this.context = _context;
        }

        public void Add(Address entity)
        {
            context.Addresses.Add(entity);
        }

        public void AddRange(IEnumerable<Address> entities)
        {
            context.Addresses.AddRange(entities);
        }

        public IEnumerable<Address> Find(Expression<Func<Address, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Address> GetAll()
        {
            return context.Addresses.ToList();
        }

        public Address? GetById(int id)
        {
            return (Address?)context.Addresses.Find(id);
        }

        public void Remove(Address entity)
        {
            context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Address> entities)
        {
            context.RemoveRange(entities);
        }
    }
}
