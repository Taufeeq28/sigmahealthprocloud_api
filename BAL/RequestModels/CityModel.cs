using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class CityModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int CityId { get; set; }

        [Required]
        public string? CityName { get; set; }

        [Required]
        public Guid? CountyId { get; set; }

        [Required]
        public Guid? StateId { get; set; }
    }
}
