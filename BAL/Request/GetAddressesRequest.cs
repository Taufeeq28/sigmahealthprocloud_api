using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Request
{
    public class GetAddressesRequest
    {

        [DefaultValue(null)]
        public string? identifier { get; set; }

        [DefaultValue(null)]
        public int? RecordCount { get; set; }
    }
}
