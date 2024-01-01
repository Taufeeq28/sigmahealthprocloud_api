using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class JuridictionModel : BaseModel
    {
        [Required]
        public string? JuridictionId { get; set; }
        [Required]
        public string? JuridictionName { get; set; }
        [Required]
        public Guid? StateId { get; set; }
        [Required]
        public Guid? AlternateId { get; set; }
    }
}
