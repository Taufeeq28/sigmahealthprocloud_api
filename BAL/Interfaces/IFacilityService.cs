﻿using BAL.Constant;
using BAL.Request;
using Data.Models;

namespace BAL.Interfaces
{
    public interface IFacilityService
    {
        Task<PaginationModel<FacilitySearchResponse>> FacilitySearch(FacilitySearchRequest request);
        Task<ApiResponse<string>> DeleteFacility(Guid facilityId);
        Task<ApiResponse<string>> CreateFacility(CreateFacilityRequest obj);
        Task<ApiResponse<string>> EditFacility(EditFacilityRequest obj);
    }
}
