using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RequestModels
{
    public class CountryModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int CountryId { get; set; }

        [Required]
        public string? CountryName { get; set; }

        [Required]
        public string? Alpha2code { get; set; }

        [Required]

        public string? Alpha3code { get; set; }
    }
}
