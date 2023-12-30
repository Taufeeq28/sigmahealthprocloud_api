
using System.ComponentModel.DataAnnotations;

namespace BAL.Request
{
    public class CreateFacilityRequest
    {
      

        [Required]
        public string? FacilityName { get; set; }

        [Required]
        public string? CreatedBy { get; set; }

        [Required]
        public string? UpdatedBy { get; set; }

        [Required]
        public Guid? OrganizationsId { get; set; }


    }
}
