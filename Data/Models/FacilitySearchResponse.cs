
using System.Text.Json.Serialization;


namespace Data.Models
{
    public class FacilitySearchResponse
    {
        //
        public Guid? id { get; set; }
        public string? facilityid { get; set; }
        public string ? jurisdiction { get; set; }
        public string? organization { get; set; }
        public string? facilityName { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? zipCode { get; set; }

        [JsonIgnore]
        public long ? TotalRows { get; set; }
    }
}
