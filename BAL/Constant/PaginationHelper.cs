using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Constant
{
    public static class PaginationHelper
    {
        public static PaginationModel<T> Paginate<T>(List<T> items, int pageNumber, int pageSize, int? totalRequestedItems = null)
        {
            var paginatedResult = new PaginationModel<T>();

            if (items != null && items.Count > 0)
            {
                int totalRows = items.Count;
                paginatedResult.TotalCount = totalRows;
                paginatedResult.TotalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                if (pageNumber > paginatedResult.TotalPages)
                {
                    pageNumber = paginatedResult.TotalPages;
                }

                int maxPageSize = paginatedResult.TotalPages > ((totalRows + paginatedResult.TotalPages - 1) / paginatedResult.TotalPages)
                    ? ((totalRows + paginatedResult.TotalPages - 1) / paginatedResult.TotalPages)
                    : 1;

                if (pageSize > maxPageSize)
                {
                    pageSize = maxPageSize;
                }

                int startIndex = (pageNumber - 1) * pageSize + 1;
                int endIndex = Math.Min(pageNumber * pageSize, totalRows);
                string pagingDetails = $"{startIndex} to {endIndex} out of {totalRows} found";
                string showingDetails = string.Empty;

                if (totalRequestedItems==null || totalRequestedItems==0)
                {
                    showingDetails = $"{totalRows} items found";
                }
                else
                {
                    showingDetails = $"{totalRows} out of {totalRequestedItems} found";
                }

                paginatedResult.CurrentPage = pageNumber;
                paginatedResult.PagingDetails = pagingDetails;
                paginatedResult.ShowingDetails = showingDetails;
                paginatedResult.Items = items.ToList();
            }

            return paginatedResult;
        }
    }

}
