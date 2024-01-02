using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class SiteModel : BaseModel
    {
        [Required]
        public string? SiteId { get; set; }
        [Required]
        public string? SiteName { get; set; }
        [Required]
        public string? SiteType { get; set; }
        [Required]
        public string? ParentSite { get; set; }
        [Required]
        public string? SiteContactPerson { get; set; }
        [Required]
        public Guid? FacilityId { get; set; }
        [Required]
        public string? FacilityName { get; set; }
        [Required]
        public string? CityName { get; set; }
        [Required]
        public string? StateName { get; set; }
        [Required]
        public string? ZipCode { get; set; }
        [Required]
        public Guid? AddressId { get; set; }
        [Required]
        public Guid? ContactId { get; set; }
        [Required]
        public bool? IsImmunizationSite { get; set; }
        [Required]
        public string? SitePinNumber { get; set; }
        public string Address { get; internal set; }
    }
}
