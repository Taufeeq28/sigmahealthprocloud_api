using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Http;
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
        [Route("GetAllOrganizations")]
        public IActionResult GetAllOrganizations()
        {
            IEnumerable<Organization> orglist = _unitOfWork.Organizations.GetAll();
            var result = orglist.Select(o =>
            new {
                OrganizationId = o.OrganizationsId,
                OrganizationName = o.OrganizationName,
                JuridictionId = o.JuridictionId

            });
            return Ok(orglist);
        }
        [HttpGet]
        [Route("GetOrganizationByJurd/{jurdid}")]
        public IActionResult GetOrganizationByJurd(string jurdid)
        {
            var orgnizationlist = _unitOfWork.Organizations.GetOrganizationByJuridictionId(jurdid);
            var result = orgnizationlist.Select(o =>
            new
            {
                OrganizationId = o.OrganizationsId,
                Organizationname = o.OrganizationName
                
            });
            return Ok(result);
        }

    }
}
