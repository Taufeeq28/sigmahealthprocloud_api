using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RequestModels
{
    public class OrganizationModel : BaseModel
    {
        [Required]
        public string OrganizationsId { get; set; }
        [Required]
        public string OrganizationName { get; set; }

        [Required]
        public Guid? JuridictionId { get; set; }

        [Required]
        public Guid? AddressId { get; set; }

    }
}
