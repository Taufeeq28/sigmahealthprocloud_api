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
        public string? UserName { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? UserRole { get; set; }
        [Required]
        public string? FacilityName { get; set; }
        [Required]
        public string? JuridictionName { get; set; }
        [Required]
        public string? imageurl { get; set; }


    }
}
