using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class Userloginmodel
    {
        [Required]
        public string? UserId { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? UserRole { get; set; }
        [Required]
        public string? FacilityName { get; set; }
        [Required]
        public string? JuridictionName { get; set; }


    }
}
