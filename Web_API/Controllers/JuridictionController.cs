using Data.Implementation;
using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JuridictionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;
        
        public JuridictionController(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            
        }
        [HttpGet]
        [Route("GetAllJuridictions")]
        public IActionResult GetAllJuridictions()
        {
           IEnumerable<Juridiction> juridiclist = _unitOfWork.Juridictions.GetAll();
            var result = juridiclist.Select(j =>
            new {
                JuridictionId = j.JuridictionId,
                Juridictionname = j.JuridictionName,

            });
            return Ok(juridiclist);
        }

        [HttpGet]
        [Route("GetJuricdictionByBus/{businessid}")]
        public IActionResult GetJuricdictionByBus(string businessid)
        {
            var juridiclist = _unitOfWork.Juridictions.GetJuridictionsbyBusinessid(businessid);
            var result = juridiclist.Select(j =>
            new
            {
                JuridictionId = j.JuridictionId,
                Juridictionname = j.JuridictionName,

            });
            return Ok(result);
        }
    }
}
