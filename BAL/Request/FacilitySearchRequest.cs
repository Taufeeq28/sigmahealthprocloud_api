
namespace BAL.Request
{
    public class FacilitySearchRequest
    {
        public List<string>? identifiers { get; set; } = new List<string>();
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public string sortBy { get; set; } = "facilityname";
        public string sortDirection { get; set; } = "desc";
        public string? searchFacilityName { get; set; } 
        public string? searchJurisdiction { get; set; }
        public string? searchOrganization { get; set; }
        public string? searchAddress { get; set; }
        public string? searchCity { get; set; }
        public string? searchState { get; set; }
        public string? searchZipCode { get; set; }
    }
}
