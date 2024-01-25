using BAL.Repository;
using BAL.RequestModels;
using BAL.Constant;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BAL.Responses;

namespace BAL.Implementation
{
    public class OrganizationsRepository : IGenericRepository<OrganizationModel>, IOrganizationsRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public OrganizationsRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger)
        {
            context = _context;
            _logger = logger;
        }

        public async Task<IEnumerable<OrganizationModel>> Find(Expression<Func<OrganizationModel, bool>> predicate)
        {
            return (IEnumerable<OrganizationModel>)await context.Set<OrganizationModel>().FindAsync(predicate);
        }

        public async Task<IEnumerable<OrganizationModel>> GetAllAsync()
        {
            try
            {
                var orgList = new List<OrganizationModel>();
                var organization = await context.Organizations.Where(o => o.Isdelete == false).ToListAsync();

                foreach (var entity in organization)
                {
                    var organizationmod = new OrganizationModel()
                    {
                        Id = entity.Id,
                        OrganizationsId = entity.OrganizationsId,
                        OrganizationName = entity.OrganizationName,
                        JuridictionId = entity.JuridictionId,
                        AddressId = entity.AddressId,
                        CreatedDate = entity.CreatedDate,
                        CreatedBy = entity.CreatedBy,
                        UpdatedBy = entity.UpdatedBy                        
                        
                    };
                    orgList.Add(organizationmod);
                }
                Task.WhenAll();

                return orgList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorelationId: {_corelationId} -Exception occurred in Method: {nameof(GetAllAsync)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }

        }

        public async Task<OrganizationModel> GetByIdAsync(int id)
        {
            try
            {
                return await context.Set<OrganizationModel>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CorelationId: {_corelationId} -Exception occurred in Method: {nameof(GetByIdAsync)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                throw;
            }
        }

        public async Task<ApiResponse<string>> InsertAsync(OrganizationModel organization)
        {
            try
            {
                var newOrg = new Organization()
                {
                    OrganizationsId = organization.OrganizationsId,
                    OrganizationName = organization.OrganizationName,
                    CreatedBy = organization.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedBy = organization.UpdatedBy,
                    UpdatedDate = DateTime.UtcNow,
                    JuridictionId = organization.JuridictionId,
                    AddressId = organization.AddressId
                };
                context.Organizations.Add(newOrg);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(newOrg.Id.ToString(), "Organization created successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while creating the organization.");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(OrganizationModel org)
        {
            if (org == null)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Invalid input. EditFacilityRequest object is null in Method: {nameof(UpdateAsync)}");
                return ApiResponse<string>.Fail("Invalid input. EditFacilityRequest object is null.");
            }
            try
            {
                var updateOrganization = await context.Organizations.FindAsync(org.Id);
                if (updateOrganization != null)
                {
                    updateOrganization.OrganizationName = org.OrganizationName;
                    updateOrganization.JuridictionId = org.JuridictionId;
                    updateOrganization.AddressId = org.AddressId;
                    updateOrganization.UpdatedBy = org.UpdatedBy;
                    updateOrganization.UpdatedDate = DateTime.UtcNow;
                    context.Organizations.Update(updateOrganization);
                    await context.SaveChangesAsync();

                    return ApiResponse<string>.Success(org.Id.ToString(), "Organization record updated successfully.");
                }
                return ApiResponse<string>.Fail("Organization with the given ID not found.");

            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(UpdateAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while creating the organization.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid orgid)
        {
            try
            {
                var Organization = await context.Organizations.FindAsync(orgid);

                if (Organization != null)
                {
                    Organization.Isdelete = true;
                    context.Organizations.Update(Organization);
                    await context.SaveChangesAsync();

                    return ApiResponse<string>.Success(orgid.ToString(), "Organization deleted successfully.");
                }

                return ApiResponse<string>.Fail("Organization with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("Organization with the given ID not found.");
            }
        }

        public async Task<ApiResponse<List<OrganizationModel>>> GetOrganizationByJuridictionId(Guid jurdid)
        {
            try
            {
                var orgList = new List<OrganizationModel>();
                var organization = await context.Organizations.Where(i => i.JuridictionId.ToString().ToLower().Equals(jurdid.ToString().ToLower()) && i.Isdelete == false)
                    .ToListAsync();
                Task.WhenAll();
                foreach (var entity in organization)
                {
                    var organizationmod = new OrganizationModel()
                    {
                        Id=entity.Id,                        
                        OrganizationsId = entity.OrganizationsId,
                        OrganizationName = entity.OrganizationName,
                        JuridictionId = entity.JuridictionId,
                        AddressId = entity.AddressId,
                        CreatedDate = entity.CreatedDate,
                        CreatedBy = entity.CreatedBy,
                        UpdatedBy=entity.UpdatedBy,
                    };
                    orgList.Add(organizationmod);
                }
                return ApiResponse<List<OrganizationModel>>.Success(orgList, "Organization details fetched successfully.");
                //return orgList;


            }
            catch (Exception ex)
            {
                _logger.LogError($"CorelationId: {_corelationId} -Exception occurred in Method: {nameof(GetOrganizationByJuridictionId)} Error: {ex?.Message}, Stack trace: {ex?.StackTrace}");
                //return ApiResponse<List<OrganizationModel>>.Fail("An error occurred while fetching the organization.");
                throw new Exception(ex?.Message);
            }
        }



    }
}
