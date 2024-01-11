using BAL.Constant;
using BAL.Request;
using BAL.Responses;

namespace BAL.Interfaces
{
    public interface IContactService
    {
        Task<ApiResponse<string>> CreateEntityContact(CreateEntityContactsRequest obj);
        Task<ApiResponse<GetContactResponse>> GetEntityContact(GetEntityAddressesRequest getContactRequest);
    }
}
