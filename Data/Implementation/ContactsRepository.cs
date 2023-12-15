using Data;
using Data.Models;
using Data.Repository;
using Microsoft.Extensions.Logging;
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
        private ILogger<UnitOfWork> _logger;
        public ContactsRepository(SigmaproIisContext _context,ILogger<UnitOfWork> logger)
        {
            this.context = _context;
            _logger = logger;
        }

        public void Add(Contact entity)
        {
            try
            {
                context.Contacts.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(Add)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public void AddRange(IEnumerable<Contact> entities)
        {
            try
            {
                context.Contacts.AddRange(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(AddRange)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public IEnumerable<Contact> Find(Expression<Func<Contact, bool>> predicate)
        {
            try
            {
                return context.Contacts.Where(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(Find)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public IEnumerable<Contact> GetAll()
        {
            try
            {
                return context.Contacts.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(GetAll)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public Contact? GetById(int id)
        {
            try
            {
                return (Contact?)context.Contacts.Find(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Method: {nameof(GetById)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public void Remove(Contact entity)
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

        public void RemoveRange(IEnumerable<Contact> entities)
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
