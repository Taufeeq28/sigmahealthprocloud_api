using BAL.Pagination;
using BAL.Repository;
using BAL.RequestModels;
using BAL.Constant;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BAL.Implementation
{
    public class SiteRepository : IGenericRepository<SiteModel>, ISiteRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public SiteRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger)
        {
            context = _context;
            _logger = logger;
        }
        public async Task<IEnumerable<SiteModel>> Find(Expression<Func<SiteModel, bool>> predicate)
        {
            return (IEnumerable<SiteModel>)await context.Set<SiteModel>().FindAsync(predicate);
        }
        public async Task<IEnumerable<SiteModel>> GetAllAsync()
        {
            return await context.Set<SiteModel>().ToListAsync();
        }
        public async Task<IEnumerable<SiteModel>> GetAllAsync(SearchParams search)
        {
            var siteModelList = new List<SiteModel>();
            string keyword = search.keyword.IsNullOrEmpty() ? string.Empty : search.keyword.Trim().ToLower();
            string facilityname = search.facility_name.IsNullOrEmpty() ? string.Empty : search.facility_name.Trim().ToLower();
            string cityname = search.city.IsNullOrEmpty() ? string.Empty : search.city.Trim().ToLower();
            string statename = search.state.IsNullOrEmpty() ? string.Empty : search.state.Trim().ToLower();
            string sitetype = search.site_type.IsNullOrEmpty() ? string.Empty : search.site_type.Trim().ToLower();
            string parentsite = search.parent_site.IsNullOrEmpty() ? string.Empty : search.parent_site.Trim().ToLower();
            string sitename = search.site_name.IsNullOrEmpty() ? string.Empty : search.site_name.Trim().ToLower();
            string sitepinnumber = search.sitepinnumber.IsNullOrEmpty() ? string.Empty : search.sitepinnumber.Trim().ToLower();
            if (keyword.IsNullOrEmpty() && facilityname.IsNullOrEmpty() && cityname.IsNullOrEmpty() && statename.IsNullOrEmpty() && sitetype.IsNullOrEmpty() && sitepinnumber.IsNullOrEmpty() && sitename.IsNullOrEmpty() && parentsite.IsNullOrEmpty())
            {
                return siteModelList;
            }

            var siteList = await context.Sites.Join(context.Facilities, st => st.FacilityId.Value.ToString(), ft => ft.Id.ToString(), (st, ft) => new { sites = st, addid = st.AddressId, facilities = ft }).
                 Join(context.Addresses, f => f.addid, a => a.Id, (f, a) => new { facility = f.facilities, f.sites, add = a }).
                 Join(context.Cities, st => st.add.CityId, ct => ct.Id, (st, ct) => new { st.facility, st.sites, st.add, cities = ct }).
                 Join(context.States, ct => ct.cities.StateId, st => st.Id, (ct, st) => new { ct.facility, ct.sites, ct.add, states = st, ct.cities }).
                 Where(i => i.sites.SiteName.ToLower().Contains(keyword) || i.facility.FacilityName.ToLower().Contains(keyword) || i.cities.CityName.ToLower().Contains(keyword)
                 || i.states.StateName.ToLower().Contains(keyword) || i.sites.SiteType.ToLower().Contains(keyword) || i.sites.ParentSite.ToLower().Contains(keyword)).
                 Where(i => i.sites.SiteName.ToLower().Contains(sitename)
                       && i.facility.FacilityName.ToLower().Contains(facilityname)
                       && i.cities.CityName.ToLower().Contains(cityname)
                       && i.states.StateName.ToLower().Contains(statename)
                       && i.sites.SiteType.ToLower().Contains(sitetype)
                       && i.sites.ParentSite.ToLower().Contains(parentsite)
                       && i.sites.SitePinNumber.ToLower().Contains(sitepinnumber)).Select(i => new
                       {
                           i.sites.SiteName,
                           i.facility.FacilityName,
                           i.cities.CityName,
                           i.states.StateName,
                           i.add.Line1,
                           i.add.Line2,
                           i.add.Suite,
                           i.sites.ParentSite,
                           i.sites.SiteType,
                           i.sites.SitePinNumber,
                           i.sites.IsImmunizationSite,
                           i.sites.SiteId,
                           i.add.ZipCode
                       }).ToPagedListAsync(search.pagenumber, search.pagesize);

            Parallel.ForEach(siteList, async i =>
            {
                var model = new SiteModel()
                {
                    Address = $"{i.Line1} {i.Line2} {i.Suite}",
                    FacilityName = i.FacilityName,
                    SiteName = i.SiteName,
                    ParentSite = i.ParentSite,
                    SiteType = i.SiteType,
                    SitePinNumber = i.SitePinNumber,
                    IsImmunizationSite = i.IsImmunizationSite,
                    SiteId = i.SiteId,
                    CityName = i.CityName,
                    StateName = i.StateName,
                    ZipCode = i.ZipCode
                };
                siteModelList.Add(model);

            });
            Task.WhenAll();
            return siteModelList;

        }

        public async Task<SiteModel> GetByIdAsync(int id)
        {
            return await context.Set<SiteModel>().FindAsync(id);
        }

        public async Task<ApiResponse<string>> InsertAsync(SiteModel sitemod)
        {

            try
            {
                var newsite = new Site()
                {
                    SiteId = sitemod.SiteId,
                    SiteName = sitemod.SiteName,            
                    SiteType = sitemod.SiteType,
                    ParentSite = sitemod.ParentSite,
                    SiteContactPerson = sitemod.SiteContactPerson,
                    SitePinNumber = sitemod.SitePinNumber,
                    IsImmunizationSite = sitemod.IsImmunizationSite,
                    FacilityId = sitemod.FacilityId,
                    AddressId = sitemod.AddressId,
                    CreatedBy = sitemod.CreatedBy,
                    CreatedDate = sitemod.CreatedDate,
                    UpdatedBy = sitemod.UpdatedBy,
                    UpdatedDate = sitemod.UpdatedDate,
                    Isdelete = sitemod.Isdelete,
                };
                context.Sites.Add(newsite);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(newsite.Id.ToString(), "Site created successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while creating the site.");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(SiteModel entity)
        {
            if (entity == null)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Invalid input. EditSiteRequest object is null in Method: {nameof(UpdateAsync)}");
                return ApiResponse<string>.Fail("Invalid input. EditSiteRequest object is null.");
            }
            try
            {
                var updateSite = new Site();// await context.Sites.FindAsync(entity.Id);
                if (entity != null)
                {
                    updateSite.AddressId = entity.AddressId;
                    updateSite.SiteName = entity.SiteName;
                    updateSite.SiteContactPerson = entity.SiteContactPerson;
                    updateSite.SitePinNumber = entity.SitePinNumber;
                    updateSite.IsImmunizationSite = entity.IsImmunizationSite;
                    updateSite.SiteType = entity.SiteType;
                    context.Sites.Update(updateSite);
                    await context.SaveChangesAsync();

                    return ApiResponse<string>.Success(updateSite.Id.ToString(), "Organization record updated successfully.");
                }
                return ApiResponse<string>.Fail("Site with the given ID not found.");

            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(UpdateAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while updating the site.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await context.Set<Site>().FindAsync(id);
                if (entity != null)
                {
                    entity.Isdelete = true;
                    context.Sites.Update(entity);
                    await context.SaveChangesAsync();
                    return ApiResponse<string>.Success(id.ToString(), "Site deleted successfully.");
                }

                return ApiResponse<string>.Fail("Site with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("Site with the given ID not found.");
            }
        }

    }
}
