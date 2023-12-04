using BAL.Interfaces;
using BAL.Request;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private IFacilityService _facilityfervice;
        public FacilityController(IFacilityService facilityfervice)
        {
            _facilityfervice = facilityfervice;
        }


        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> FacilitySearch([FromBody] FacilitySearchRequest obj)
         =>Ok(await _facilityfervice.FacilitySearch(obj).ConfigureAwait(true));
    }
}
