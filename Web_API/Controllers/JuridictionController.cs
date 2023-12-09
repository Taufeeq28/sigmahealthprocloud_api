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
        [Route("getalljuridictions")]
        public IActionResult GetAllJuridictions()
        {
           IEnumerable<Juridiction> juridictionlist = _unitOfWork.Juridictions.GetAll();
            var Juridictions = juridictionlist.Select(j =>
            new {
                JuridictionId = j.Id,
                Juridictionname = j.JuridictionName,

            });
            return Ok(Juridictions);
        }

        [HttpGet]
        [Route("getjuricdictionbybusiness")]
        public IActionResult GetJuricdictionByBusiness(string businessid)
        {
            var juridictionlist = _unitOfWork.Juridictions.GetJuridictionsbyBusinessid(businessid);
            var Juridictions = juridictionlist.Select(j =>
            new
            {
                JuridictionId = j.Id,
                Juridictionname = j.JuridictionName,

            });
            return Ok(Juridictions);
        }
    }
}
