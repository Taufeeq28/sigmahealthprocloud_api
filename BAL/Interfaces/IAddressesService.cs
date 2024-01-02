using BAL.Responses;
using BAL.Constant;
using BAL.Request;

namespace BAL.Interfaces
{
        public interface IAddressesService
        {
            Task<ApiResponse<GetAddressesResponse>> GetAddresses(GetAddressesRequest getAddressesRequest);
            Task<ApiResponse<string>> CreateEntityAddress(CreateEntityAddressRequest obj);
        }
    
}
