using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Request
{
    public class GenerateNextIdRequest
    {
        public string? output_table_name { get; set; }
        public string? start_column_name { get; set; }
        public string? suffix_column_name { get; set; }
        public string? output_column_name { get; set; }
    }
}
