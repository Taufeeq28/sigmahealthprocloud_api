

namespace BAL.Responses
{
    public class GetAddressesResponse
    {
        public Guid Id { get; set; }
        public string? AddressId { get; set; }
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? Suite { get; set; }
        public string? CountyName { get; set; }
        public string? CountryName { get; set; }
        public string? StateName { get; set; }
        public string? CityName { get; set; }
        public string? ZipCode { get; set; }
        public string? FullAddress { get; set; }

    }
}