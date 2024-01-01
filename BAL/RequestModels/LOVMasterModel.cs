using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class LOVMasterModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string? Key { get; set; }

        [Required]
        public string? Value { get; set; }

        [Required]
        public string? LovType { get; set; }

        [Required]
        public string? LongDescription { get; set; }
    }
}
