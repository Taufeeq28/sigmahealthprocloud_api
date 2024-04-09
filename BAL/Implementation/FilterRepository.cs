using BAL.Repository;
using BAL.RequestModels;
using Data.Models;
using Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace BAL.Implementation
{
    public class FilterRepository : IFilterRepository

    {
        private SigmaproIisContext _context;
        private ILogger<UnitOfWork> _logger;
    
        public FilterRepository(SigmaproIisContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
            
        }
        public async Task<IEnumerable<FilterModel>> FindFiltersAsync(string pageName)
        {
            return await _context.Filters
                .Where(f => f.PageName.Equals(pageName, StringComparison.OrdinalIgnoreCase))
                .Select(f => new FilterModel
                {
                    PageName = f.PageName,
                    FilterType = f.FilterType,
                    FilterCondition = f.FilterCondition,
                    LogicalOperator = f.LogicalOperator
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<dynamic>> ApplyFiltersAsync(string pageName, string filterType, string filterCondition)
        {
            var query = GetQueryableForPageName(pageName);

            if (query == null)
            {
                throw new ArgumentException("The specified page name does not correspond to a known table.", nameof(pageName));
            }

            var convertedFilterType = ConvertToPascalCase(filterType);

            // Directly apply the dynamic filter condition
            // This assumes convertedFilterType represents a valid C# property name and filterCondition is the value to search for
            var dynamicFilter = $"{convertedFilterType}.ToLower().Contains(@0)";
            query = query.Where(dynamicFilter, filterCondition.ToLower());

            return await DynamicToListAsync(query);
        }

        private IQueryable GetQueryableForPageName(string pageName)
        {
          
            switch (pageName.ToLower())
            {
                case "patients":
                    return _context.Patients.AsQueryable();
                case "facilities":
                    return _context.Facilities.AsQueryable();
                case "events":
                    return _context.Events.AsQueryable();
                case "sites":
                    return _context.Sites.AsQueryable();
             
                default:
                    return null;
            }
        }
        private string ConvertToPascalCase(string text)
        {
            return string.Concat(text.Split('_').Select(word => char.ToUpper(word[0]) + word.Substring(1)));
        }



        private async Task<List<dynamic>> DynamicToListAsync(IQueryable query)
        {
            return await query.ToDynamicListAsync();
        }
        
    }
}
