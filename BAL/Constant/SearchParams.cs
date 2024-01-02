using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Constant
{
    public class SearchParams
    {
        public string sitepinnumber;
        public string keyword { get; set; }
        public string facilityid { get; set; }
        public string facility_name { get; set; }
        public int pagenumber { get; set; }
        public int pagesize { get; set; }
        public string site_name { get; set; }
        public string site_type { get; set; }
        public string parent_site { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string userid { get; set; }
        public string usertype { get; set; }
        public string orderby { get; set; }

    }
}
