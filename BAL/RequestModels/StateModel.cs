using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class StateModel
    {

        [Required]
        public Guid Id { get; set; }
        [Required]
        public int StateId { get; set; }

        [Required]
        public string? StateName { get; set; }

        [Required]
        public string? StateCode { get; set; }

        [Required]
        public Guid? CountryId { get; set; }
    }
}
