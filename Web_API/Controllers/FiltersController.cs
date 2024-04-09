using BAL.Repository;
using BAL.RequestModels;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiltersController : ControllerBase
    {
        private readonly IFilterRepository _filterRepository;
        public FiltersController(IFilterRepository filterRepository)
        {
            _filterRepository = filterRepository;
        }



        // POST: api/filter/apply
        [HttpPost]
        public async Task<IActionResult> ApplyFilters([FromBody] FilterApplyRequest request)
        {
            try
            {
                // Assuming the filter application method on the repository is able to handle these directly
                var result = await _filterRepository.ApplyFiltersAsync(request.PageName, request.FilterType, request.FilterCondition);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
    public class FilterApplyRequest
    {
        public string PageName { get; set; }
        public string FilterType { get; set; }
        public string FilterCondition { get; set; }
    }
}
