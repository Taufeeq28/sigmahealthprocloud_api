using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Constant
{
    public class SearchOrderParams
    {
        public string keyword { get; set; }
        public int pagenumber { get; set; }
        public int pagesize { get; set; }
        public string date_of_order { get; set; }
        public string order_status { get; set; }
        public string order_item_desc { get; set; }


    }
}
