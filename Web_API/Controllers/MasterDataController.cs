using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;
        public MasterDataController(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;

        }
        #region Countries
        [HttpGet]
        [Route("getallcountries")]
        public IActionResult GetAllCountries()
        {
            IEnumerable<Country> countrylist = _unitOfWork.Countries.GetAll();
            var countries = countrylist.Select(cu =>
            new {
                Id = cu.Id,
                CountryId = cu.CountryId,
                CountryName = cu.CountryName,

            });
            return Ok(countries);
        }
        #endregion

        #region States
        [HttpGet]
        [Route("getallstates")]
        public IActionResult GetAllStates()
        {
            IEnumerable<State> statelist = _unitOfWork.States.GetAll();
            var States = statelist.Select(s =>
            new {
                Id = s.Id,
                StateId = s.StateId,
                StateName = s.StateName,

            });
            return Ok(States);
        }

        [HttpGet]
        [Route("getstatesbycountryid/{countryid}")]
        public IActionResult GetStatesbyCountryid(string countryid)
        {
            IEnumerable<State> statelist = _unitOfWork.States.Find(pre => pre.CountryId.Value.ToString().ToLowerInvariant().Equals(countryid.ToLowerInvariant())).ToList();
            var States = statelist.Select(s =>
            new {
                Id = s.Id,
                StateId = s.StateId,
                StateName = s.StateName,

            });
            return Ok(States);
        }

        #endregion

        #region Counties
        [HttpGet]
        [Route("getallcounties")]
        public IActionResult GetAllCounties()
        {
            IEnumerable<County> countieslist = _unitOfWork.Counties.GetAll();
            var Counties = countieslist.Select(cu =>
            new {
                Id = cu.Id,
                CountyId = cu.CountyId,
                CountyName = cu.CountyName,

            });
            return Ok(Counties);
        }

        [HttpGet]
        [Route("getallcountiesbystateid")]
        public IActionResult GetAllCountiesByStateId(string stateid)
        {
            IEnumerable<County> countieslist = _unitOfWork.Counties.Find(pre => pre.StateId.Value.ToString().ToLowerInvariant().Equals(stateid.ToLowerInvariant())).ToList();
            var counties = countieslist.Select(cu =>
            new {
                Id = cu.Id,
                CountyId = cu.CountyId,
                CountyName = cu.CountyName,

            });
            return Ok(counties);
        }
        #endregion

        #region Cities
        [HttpGet]
        [Route("getallcities")]
        public IActionResult GetAllCities()
        {
            IEnumerable<City> citieslist = _unitOfWork.Cities.GetAll();
            var Cities = citieslist.Select(cu =>
            new {
                Id = cu.Id,
                CityId = cu.CityId,
                CityName = cu.CityName,

            });
            return Ok(Cities);
        }

        [HttpGet]
        [Route("getallcitiesbystateandcountyId")]
        public IActionResult GetAllCitiesByStateId(string stateid, string countyid)
        {
            IEnumerable<City> citieslist = _unitOfWork.Cities.Find(pre => !(!pre.StateId.Value.ToString().ToLowerInvariant().Equals(stateid.ToLowerInvariant()) || !pre.CountyId.Value.ToString().ToLowerInvariant().Equals(countyid.ToLowerInvariant()))).ToList();
            var Cities = citieslist.Select(cu =>
            new {
                Id = cu.Id,
                CityId = cu.CityId,
                CityName = cu.CityName

            });
            return Ok(Cities);
        }

        [HttpGet]
        [Route("getallcitiesbycountyId")]
        public IActionResult GetAllCitiesByCountyId(string countyid)
        {
            IEnumerable<City> citieslist = _unitOfWork.Cities.Find(pre => !(!pre.CountyId.Value.ToString().ToLowerInvariant().Equals(countyid.ToLowerInvariant()))).ToList();
            var Cities = citieslist.Select(cu =>
            new {
                Id = cu.Id,
                CityId = cu.CityId,
                CityName = cu.CityName

            });
            return Ok(Cities);
        }


        #endregion

    }
}
