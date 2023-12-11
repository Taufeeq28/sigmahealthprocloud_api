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
    public class ContactsRepository : IGenericRepository<Contact>, IContactsRepository
    {
        private SigmaproIisContext context;
        public ContactsRepository(SigmaproIisContext _context)
        {
            this.context = _context;
        }

        public void Add(Contact entity)
        {
            context.Contacts.Add(entity);
        }

        public void AddRange(IEnumerable<Contact> entities)
        {
            context.Contacts.AddRange(entities);
        }

        public IEnumerable<Contact> Find(Expression<Func<Contact, bool>> predicate)
        {
            return context.Contacts.Where(predicate);
        }

        public IEnumerable<Contact> GetAll()
        {
            return context.Contacts.ToList();
        }

        public Contact? GetById(int id)
        {
            return (Contact?)context.Contacts.Find(id);
        }

        public void Remove(Contact entity)
        {
            context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Contact> entities)
        {
            context.RemoveRange(entities);
        }
    }
}
