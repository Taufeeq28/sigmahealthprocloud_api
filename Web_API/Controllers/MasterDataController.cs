using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;
        private readonly ILogger<MasterDataController> _logger;
        public MasterDataController(IUnitOfWork unitOfWork, IConfiguration config, ILogger<MasterDataController> logger)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _logger = logger;
        }
        #region Countries
        [HttpGet]
        [Route("getallcountries")]
        public IActionResult GetAllCountries()
        {
            try
            {
                IEnumerable<Country> countrylist = _unitOfWork.Countries.GetAll();
                if (countrylist != null && countrylist.Any())
                {
                    var countries = countrylist.Select(cu =>
                    new
                    {
                        Id = cu.Id,
                        CountryId = cu.CountryId,
                        CountryName = cu.CountryName,

                    });
                    return Ok(countries);
                }
                return NotFound($"No data found for Countries");

            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while processing GetAllCountries request.");

                // Return a 500 Internal Server Error with a generic message
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        #endregion

        #region States
        [HttpGet]
        [Route("getallstates")]
        public IActionResult GetAllStates()
        {
            try
            {
                IEnumerable<State> statelist = _unitOfWork.States.GetAll();
                if (statelist != null && statelist.Any())
                {
                    var States = statelist.Select(s =>
                    new
                    {
                        Id = s.Id,
                        StateId = s.StateId,
                        StateName = s.StateName,

                    });
                    return Ok(States);
                }
                return NotFound($"No data found for States");

            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while processing GetAllStates request.");

                // Return a 500 Internal Server Error with a generic message
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("getstatesbycountryid")]
        public IActionResult GetStatesbyCountryid([FromQuery, Required] string countryid)
        {
            try
            {
                IEnumerable<State> statelist = _unitOfWork.States.Find(pre => pre.CountryId.Value.ToString().ToLower().Equals(countryid) && pre.Isdelete == false).ToList();
                if (statelist != null && statelist.Any())
                {

                    var States = statelist.Select(s =>
                    new
                    {
                        Id = s.Id,
                        StateId = s.StateId,
                        StateName = s.StateName,

                    });
                    return Ok(States);
                }
                return NotFound($"No states found for countryID {countryid}");
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, $"An error occurred while processing GetStatesbyCountryid request for countryid {countryid}.");

                //Return a 500 Internal Server Error with a generic message
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }

        #endregion

        #region Counties
        [HttpGet]
        [Route("getallcounties")]
        public IActionResult GetAllCounties()
        {
            try
            {
                IEnumerable<County> countieslist = _unitOfWork.Counties.GetAll();
                if (countieslist != null && countieslist.Any())
                {
                    var Counties = countieslist.Select(cu =>
                    new
                    {
                        Id = cu.Id,
                        CountyId = cu.CountyId,
                        CountyName = cu.CountyName,

                    });
                    return Ok(Counties);
                }
                return NotFound($"No data found for Counties");

            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while processing GetAllCounties request.");

                // Return a 500 Internal Server Error with a generic message
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("getallcountiesbystateid")]
        public IActionResult GetAllCountiesByStateId([FromQuery, Required] Guid stateid)
        {
            try
            {
                IEnumerable<County> countieslist = _unitOfWork.Counties.Find(pre => pre.StateId.Value.ToString().ToLower().Equals(stateid.ToString()) && pre.Isdelete == false).ToList();
                if (countieslist != null && countieslist.Any())
                {
                    var counties = countieslist.Select(cu =>
                    new
                    {
                        Id = cu.Id,
                        CountyId = cu.CountyId,
                        CountyName = cu.CountyName,

                    });
                    return Ok(counties);
                }
                return NotFound($"No counties found for stateid {stateid}");
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while processing GetAllCountiesByStateId request.");

                // Return a 500 Internal Server Error with a generic message
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        #endregion

        #region Cities

        [HttpGet]
        [Route("getallcitiesbystateandcountyId")]
        public IActionResult GetAllCitiesByStateId([FromQuery, Required] string stateid, [FromQuery, Required] string countyid)
        {
            try
            {
                IEnumerable<City> citieslist = _unitOfWork.Cities.Find(pre => pre.StateId.Value.ToString().ToLower().Equals(stateid) && pre.CountyId.Value.ToString().ToLower().Equals(countyid) && pre.Isdelete == false).ToList();
                if (citieslist != null && citieslist.Any())
                {
                    var Cities = citieslist.Select(cu =>
                    new
                    {
                        Id = cu.Id,
                        CityId = cu.CityId,
                        CityName = cu.CityName

                    });
                    return Ok(Cities);
                }
                return NotFound($"No cities found for stateid {stateid} and countyid {countyid}");
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while processing GetAllCitiesByStateId request.");

                // Return a 500 Internal Server Error with a generic message
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("getallcitiesbycountyId")]
        public IActionResult GetAllCitiesByCountyId([FromQuery, Required] string countyid)
        {
            try
            {
                IEnumerable<City> citieslist = _unitOfWork.Cities.Find(pre => (pre.CountyId.Value.ToString().ToLower().Equals(countyid)) && pre.Isdelete == false).ToList();
                if (citieslist != null && citieslist.Any())
                {
                    var Cities = citieslist.Select(cu =>
                    new
                    {
                        Id = cu.Id,
                        CityId = cu.CityId,
                        CityName = cu.CityName

                    });
                    return Ok(Cities);
                }
                return NotFound($"No cities found for countyid {countyid}");
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while processing GetAllCitiesByCountyId request.");

                // Return a 500 Internal Server Error with a generic message
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        #endregion

        #region LovMaster
        [HttpGet]
        [Route("getlovmasterbylovtype")]
        public IActionResult GetLOVMasterbyLOVTypeid([FromQuery, Required] string lovtype)
        {
            try
            {
                IEnumerable<LovMaster> lovmasterlist = _unitOfWork.lOVTypeMaster.Find(pre => pre.LovType.Equals(lovtype) && pre.Isdelete == false).ToList();
                if (lovmasterlist != null && lovmasterlist.Any())
                {
                    var LOVMaster = lovmasterlist.Select(l =>
                    new
                    {
                        Id = l.Id,
                        Key = l.Key,
                        Value = l.Value,
                        isdelete = l.Isdelete

                    });
                    return Ok(LOVMaster);
                }
                return NotFound($"No record found for Lovtype {lovtype}");
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while processing GetLOVMasterbyLOVTypeid request.");

                // Return a 500 Internal Server Error with a generic message
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        #endregion

    }
}
