using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RequestModels
{
    public class CountyModel
    {

        [Required]
        public Guid Id { get; set; }
        [Required]
        public int CountyId { get; set; }
        [Required]
        public string? CountyName { get; set; }
        [Required]
        public string? CountyCode { get; set; }
        [Required]
        public Guid? StateId { get; set; }
    }
}
