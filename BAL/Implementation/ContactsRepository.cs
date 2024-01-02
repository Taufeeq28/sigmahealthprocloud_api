using BAL.Constant;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BAL.RequestModels;
using BAL.Repository;

namespace BAL.Implementation
{
    public class ContactsRepository : IGenericRepository<Contact>, IContactsRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public ContactsRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger)
        {
            context = _context;
            _logger = logger;
        }

        public async Task<IEnumerable<Contact>> Find(Expression<Func<Contact, bool>> predicate)
        {
            return await context.Contacts.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await context.Set<Contact>().ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            return await context.Set<Contact>().FindAsync(id);
        }

        public async Task<ApiResponse<string>> InsertAsync(Contact entity)
        {
            try
            {
                await context.Set<Contact>().AddAsync(entity);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "Contact inserted successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Inserting the Contact.");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(Contact entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "Contact Updated successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Updating the Contact.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await context.Set<Contact>().FindAsync(id);
                if (entity != null)
                {
                    context.Set<Contact>().Remove(entity);
                    await context.SaveChangesAsync();
                    return ApiResponse<string>.Success(id.ToString(), "Contact deleted successfully.");
                }

                return ApiResponse<string>.Fail("Contact with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("Contact with the given ID not found.");
            }
        }

        public async Task<List<ContactsModel>> GetContactsbyContactid(Guid contactid)
        {
            try
            {
                var contactlist = new List<ContactsModel>();
                var contacts = await context.Contacts.Where(s => s.Id.ToString().ToLower().Equals(contactid.ToString().ToLower()) && s.Isdelete == false).ToListAsync();
                foreach (var l in contacts)
                {
                    var contactsmod = new ContactsModel()
                    {
                        Id = l.Id,
                        ContactsId = l.ContactsId,
                        ContactType = l.ContactType,
                        ContactValue = l.ContactValue,
                        EntityId = l.EntityId,
                        EntityType=l.EntityType

                    };
                    contactlist.Add(contactsmod);
                }

                return contactlist;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorelationId: {_corelationId} -Exception occurred in Method: {nameof(GetContactsbyContactid)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw new Exception(ex.Message); ;
            }

        }
    }
}
