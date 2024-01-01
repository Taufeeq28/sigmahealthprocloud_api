using BAL.Repository;
using BAL.Implementation;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JuridictionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;
        private readonly ILogger<JuridictionController> _logger;

        public JuridictionController(IUnitOfWork unitOfWork, IConfiguration config, ILogger<JuridictionController> logger)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _logger = logger;
        }
        [HttpGet]
        [Route("getalljuridictions")]
        public async Task<IActionResult> GetAllJuridictions()
             => Ok(await _unitOfWork.Juridictions.GetAllAsync().ConfigureAwait(true));

        [HttpGet]
        [Route("getjuricdictionbybusiness")]
        public async Task<IActionResult> GetJuricdictionByBusiness([FromQuery,Required] Guid businessid)
             => Ok(await _unitOfWork.Juridictions.GetJuridictionsbyBusinessid(businessid).ConfigureAwait(true));
                

    }
}
