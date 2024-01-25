using BAL.Constant;
using BAL.Responses;

namespace BAL.Interfaces
{
    public interface IPatientService
    {
        //Task<PaginationModel<FacilitySearchResponse>> FacilitySearch(FacilitySearchRequest request);
        Task<ApiResponse<PatientDetailsResponse>> GetPatientDetailsById(Guid patientId);
    }
}
