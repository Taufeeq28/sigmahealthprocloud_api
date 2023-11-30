using BAL.Interface;
using BAL.Request;
using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Search( FacilitySearchRequest obj)
        {
            IActionResult response = Unauthorized();
            return response;

        }

    }
}
