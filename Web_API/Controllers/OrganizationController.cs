using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;
        private readonly ILogger<OrganizationController> _logger;

        public OrganizationController(IUnitOfWork unitOfWork, IConfiguration config, ILogger<OrganizationController> logger)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _logger = logger;

        }
        [HttpGet]
        [Route("getallorganizations")]
        public IActionResult getallorganizations()
        {
            try
            {
                IEnumerable<Organization> organizationlist = _unitOfWork.Organizations.GetAll();
                if (organizationlist != null && organizationlist.Any())
                {
                    var Organizations = organizationlist.Select(o =>
                    new
                    {
                        OrganizationId = o.Id,
                        OrganizationName = o.OrganizationName,
                        JuridictionId = o.JuridictionId

                    });
                    return Ok(Organizations);
                }
                return NotFound($"No data found for Organizations");

            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while processing getallorganizations request.");

                // Return a 500 Internal Server Error with a generic message
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpGet]
        [Route("getorganizationbyjurdiction")]
        public IActionResult GetOrganizationByJurdiction([FromQuery, Required] string jurdictionid)
        {
            try
            {
                var orgnizationlist = _unitOfWork.Organizations.GetOrganizationByJuridictionId(jurdictionid);
                if (orgnizationlist != null && orgnizationlist.Any())
                {
                    var Organizations = orgnizationlist.Select(o =>
                    new
                    {
                        OrganizationId = o.Id,
                        Organizationname = o.OrganizationName

                    });
                    return Ok(Organizations);
                }
                return NotFound($"No Organizations found for jurdiction{jurdictionid}");
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while processing GetOrganizationByJurdiction request.");

                // Return a 500 Internal Server Error with a generic message
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}
