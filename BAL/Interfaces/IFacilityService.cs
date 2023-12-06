using BAL.Constant;
using BAL.Request;
using Data.Models;

namespace BAL.Interfaces
{
    public interface IFacilityService
    {
        Task<PaginationModel<FacilitySearchResponse>> FacilitySearch(FacilitySearchRequest request);
        Task<ApiResponse<bool>> DeleteFacility(Guid facilityId);
    }
}
