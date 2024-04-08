using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class FilterModel : BaseModel
    {

        public string? PageName { get; set; }

        public string? FilterType { get; set; }

        public string? FilterCondition { get; set; }

        public string? LogicalOperator { get; set; }
    }
}
