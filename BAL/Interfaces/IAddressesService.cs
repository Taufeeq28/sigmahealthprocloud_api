using BAL.Responses;
using BAL.Constant;
using BAL.Request;

namespace BAL.Interfaces
{
        public interface IAddressesService
        {
            Task<ApiResponse<GetAddressesResponse>> GetAddresses(GetAddressesRequest getAddressesRequest);
            Task<ApiResponse<string>> CreateEntityAddress(CreateEntityAddressRequest obj);
            Task<ApiResponse<GetAddressesResponse>> GetEntityAddresses(GetEntityAddressesRequest getAddressesRequest);
            Task<ApiResponse<string>> UpdateEntityAddress(UpdateEntityAddressRequest obj);
            Task<ApiResponse<string>> DeleteEntityAddress(Guid entityAddressId);
            Task<ApiResponse<string>> CreateMasterAddress(CreateMasterAddressRequest obj);
        }
    
}
