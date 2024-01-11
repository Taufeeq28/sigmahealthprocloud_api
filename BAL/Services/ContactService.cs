using BAL.Interfaces;
using Data.Models;
using Data;
using Microsoft.Extensions.Logging;
using BAL.Constant;
using BAL.Request;
using BAL.Responses;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace BAL.Services
{
    public class ContactService : DataAccessProvider<Contact>, IContactService
    {
        private readonly SigmaproIisContext _dbContext;
        private readonly SigmaproIisContextUdf _dbContextudf;
        private readonly ILogger<ContactService> _logger;
        private readonly string _correlationId = string.Empty;

        public ContactService(SigmaproIisContext dbContext, ILogger<ContactService> logger, SigmaproIisContextUdf dbContextudf) : base(dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
            _dbContextudf = dbContextudf;
        }

        public async Task<ApiResponse<string>> CreateEntityContact(CreateEntityContactsRequest obj)
        {
            try
            {
                Random on = new Random();

                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {

                        var newCreateEntityContact = new Contact
                        {
                            
                            ContactsId=on.Next(2).ToString(),
                            ContactValue= obj.ContactValue,
                            CreatedDate = DateTime.UtcNow,
                            CreatedBy = obj.CreatedBy,
                            Isdelete = false,
                            ContactType = obj.ContactType,
                            EntityId = obj.EntityId,
                            EntityType= obj.EntityType,
                            UpdatedDate = DateTime.UtcNow,
                           
                        };

                        _dbContext.Contacts.Add(newCreateEntityContact);

                        await _dbContext.SaveChangesAsync();

                        transaction.Commit();

                        return ApiResponse<string>.Success(null, $"Entity contact inserted successfully.");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }


            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_correlationId} - Exception occurred in Method: {nameof(CreateEntityContact)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while creating entity contact.");
            }
        }


        public async Task<ApiResponse<GetContactResponse>> GetEntityContact(GetEntityAddressesRequest getContactRequest)
        {
            try
            {
                var normalizedIdentifier = getContactRequest.Identifier?.ToLower() ?? string.Empty;
                var formattedIdentifier = $"%{normalizedIdentifier}%";

                var query = from Contact in _dbContext.Set<Contact>()
                            where (Contact.EntityId == getContactRequest.EntityId && Contact.Isdelete==false &&
                                   (string.IsNullOrWhiteSpace(getContactRequest.Identifier) ||
                                    EF.Functions.Like(Contact.ContactValue.ToLower(), formattedIdentifier) ||
                                    EF.Functions.Like(Contact.CreatedDate.ToString(), formattedIdentifier) ||
                                    EF.Functions.Like(Contact.CreatedBy.ToLower(), formattedIdentifier) ||
                                    EF.Functions.Like(Contact.ContactType.ToLower(), formattedIdentifier)))
                            select new GetContactResponse
                            {
                                Id = Contact.Id,
                                ContactValue = Contact.ContactValue,
                                CreatedDate = Contact.CreatedDate,
                                CreatedBy = Contact.CreatedBy,
                                ContactType = Contact.ContactType
                            };

                var resultQuery = query.OrderBy(a => a.CreatedDate);

                var result = await resultQuery.ToListAsync();

                result = result.Take(getContactRequest.RecordCount ?? 500).ToList();

                return ApiResponse<GetContactResponse>.SuccessList(result, "Contact data fetched successfully!");
            }
            catch (DbException ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Database exception: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<GetContactResponse>.Fail($"A database error occurred while fetching contacts: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorrelationId: {_correlationId} - Exception occurred in GetAddresses: {ex.Message}, Stack trace: {ex.StackTrace}");
                return ApiResponse<GetContactResponse>.Fail($"An error occurred while fetching addresses: {ex.Message}");
            }
        }

    }
}
