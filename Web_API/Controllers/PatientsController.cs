using BAL.Constant;
using BAL.Interfaces;
using BAL.Repository;
using BAL.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;
        private readonly ILogger<PatientsController> _logger;
        private IPatientService _patientService;
        public PatientsController(IUnitOfWork unitOfWork, IConfiguration config, ILogger<PatientsController> logger, IPatientService patientService)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _logger = logger;
            _patientService = patientService;

        }
        [HttpPost]
        [Route("createpatient")]
        public async Task<IActionResult> InsertPatient(PatientModel model) =>
            Ok(await _unitOfWork.Patients.InsertAsync(model).ConfigureAwait(true));

        [HttpPut]
        [Route("updatepatient")]
        public async Task<IActionResult> UpdatePatient(PatientModel model) =>
            Ok(await _unitOfWork.Patients.UpdateAsync(model).ConfigureAwait(true));

        [HttpPost]
        [Route("searchpatient")]
        public async Task<IActionResult> SearchPatient(SearchPatientParams model) =>
            Ok(await _unitOfWork.Patients.GetAllAsync(model).ConfigureAwait(true));

        [HttpPut]
        [Route("deletepatient")]
        public async Task<IActionResult> DeletePatient([FromForm, Required] Guid patientId) =>
            Ok(await _unitOfWork.Patients.DeleteAsync(patientId).ConfigureAwait(true));

        [HttpGet]
        [Route("patientDetailsById")]
        public async Task<IActionResult> GetPatientDetailsById(Guid patientId) =>
            Ok(await _patientService.GetPatientDetailsById(patientId).ConfigureAwait(true));
    }
}
