using BAL.Repository;
using BAL.Request;
using BAL.RequestModels;
using BAL.Implementation;
using Data.Models;
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
        public async Task<IActionResult> GetAllOrganizations()
            => Ok(await _unitOfWork.Organizations.GetAllAsync().ConfigureAwait(true));
        [HttpGet]
        [Route("getorganizationbyjurdiction")]
        public async Task<IActionResult> GetOrganizationByJurdiction([FromQuery, Required] Guid jurdictionid)
            => Ok(await _unitOfWork.Organizations.GetOrganizationByJuridictionId(jurdictionid).ConfigureAwait(true));
        
        [HttpPut]
        [Route("delete")]
        public async Task<IActionResult> DeleteOrganization([FromForm, Required] Guid orgid)
         => Ok(await _unitOfWork.Organizations.DeleteAsync(orgid).ConfigureAwait(true));

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateOrganization([FromBody] OrganizationModel obj)
         => Ok(await _unitOfWork.Organizations.InsertAsync(obj).ConfigureAwait(true));

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> EditOrganization([FromBody] OrganizationModel obj)
         => Ok(await _unitOfWork.Organizations.UpdateAsync(obj).ConfigureAwait(true));

    }
}
