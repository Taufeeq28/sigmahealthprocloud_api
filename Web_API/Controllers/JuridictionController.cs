using Data.Implementation;
using Data.Models;
using Data.Repository;
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
        public IActionResult GetAllJuridictions()
        {
            try
            {
                IEnumerable<Juridiction> juridictionlist = _unitOfWork.Juridictions.GetAll();
                if (juridictionlist != null && juridictionlist.Any())
                {
                    var Juridictions = juridictionlist.Select(j =>
                    new
                    {
                        JuridictionId = j.Id,
                        Juridictionname = j.JuridictionName,

                    });
                    return Ok(Juridictions);
                }
                return NotFound($"No data found for Juridictions");

            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while processing GetAllJuridictions request.");

                // Return a 500 Internal Server Error with a generic message
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("getjuricdictionbybusiness")]
        public IActionResult GetJuricdictionByBusiness([FromQuery,Required] string businessid)
        {
            try
            {
                var juridictionlist = _unitOfWork.Juridictions.GetJuridictionsbyBusinessid(businessid);
                if (juridictionlist != null && juridictionlist.Any())
                {
                    var Juridictions = juridictionlist.Select(j =>
                    new
                    {
                        JuridictionId = j.Id,
                        Juridictionname = j.JuridictionName,

                    });
                    return Ok(Juridictions);
                }
                return NotFound($"No data found for Juridictions for businesss{businessid}");

            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while processing GetJuricdictionByBusiness request.");

                // Return a 500 Internal Server Error with a generic message
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
