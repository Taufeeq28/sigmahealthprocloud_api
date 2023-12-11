using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;

        public OrganizationController(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;

        }
        [HttpGet]
        [Route("getallorganizations")]
        public IActionResult getallorganizations()
        {
            IEnumerable<Organization> organizationlist = _unitOfWork.Organizations.GetAll();
            var Organizations = organizationlist.Select(o =>
            new {
                OrganizationId = o.Id,
                OrganizationName = o.OrganizationName,
                JuridictionId = o.JuridictionId

            });
            return Ok(Organizations);
        }
        [HttpGet]
        [Route("getorganizationbyjurdiction")]
        public IActionResult GetOrganizationByJurdiction([FromQuery]string jurdictionid)
        {
            var orgnizationlist = _unitOfWork.Organizations.GetOrganizationByJuridictionId(jurdictionid);
            var Organizations = orgnizationlist.Select(o =>
            new
            {
                OrganizationId = o.Id,
                Organizationname = o.OrganizationName
                
            });
            return Ok(Organizations);
        }

    }
}
