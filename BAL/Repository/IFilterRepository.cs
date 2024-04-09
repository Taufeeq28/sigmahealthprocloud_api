using BAL.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repository
{
    public interface IFilterRepository 
    {
        Task<IEnumerable<FilterModel>> FindFiltersAsync(string pageName);
        Task<IEnumerable<dynamic>> ApplyFiltersAsync(string pageName, string filterType, string filterCondition);
    }
}
