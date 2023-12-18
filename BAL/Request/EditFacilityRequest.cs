using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Request
{
    public class EditFacilityRequest
    {
        [Required]
        public Guid? Id { get; set; }


        [Required]
        public string? FacilityName { get; set; }

        public string? AdministeredAtLocation { get; set; } = null;
        public string? SendingOrganization { get; set; } = null;
        public string? ResponsibleOrganization { get; set; } = null;


        [Required]
        public string? UpdatedBy { get; set; }

        [Required]
        public Guid? OrganizationsId { get; set; }

        [Required]
        public Guid? AddressId { get; set; }
    }
}
