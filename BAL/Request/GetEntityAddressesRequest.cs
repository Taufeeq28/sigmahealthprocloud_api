using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Request
{
    public class GetEntityAddressesRequest
    {
        [Required]
        public Guid EntityId { get; set; }

        [DefaultValue(null)]
        public string? Identifier { get; set; }

        [DefaultValue(null)]
        public int? RecordCount { get; set; }
    }
}
