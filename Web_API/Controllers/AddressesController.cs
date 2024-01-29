using BAL.Interfaces;
using BAL.Request;
using BAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private IConfiguration _config;
        private readonly ILogger<AddressesController> _logger;
        private IAddressesService _addressesService;
        public AddressesController(IConfiguration config, ILogger<AddressesController> logger, IAddressesService addressesService)
        {

            _config = config;
            _logger = logger;
            _addressesService = addressesService;
        }

        [HttpPost]
        [Route("create-master-addresses")]
        public async Task<IActionResult> CreateMasterAddress([FromBody] CreateMasterAddressRequest createMasterAddressRequest)

        => Ok(await _addressesService.CreateMasterAddress(createMasterAddressRequest).ConfigureAwait(true));

        [HttpPost]
        [Route("get-addresses")]
        public async Task<IActionResult> GetAddresses([FromBody] GetAddressesRequest getAddressesRequest)

          => Ok(await _addressesService.GetAddresses(getAddressesRequest).ConfigureAwait(true));

        [HttpPost]
        [Route("create-entity-addresses")]
        public async Task<IActionResult> CreateEntityAddress([FromBody] CreateEntityAddressRequest createEntityAddressRequest)

         => Ok(await _addressesService.CreateEntityAddress(createEntityAddressRequest).ConfigureAwait(true));

        [HttpPost]
        [Route("get-entity-addresses")]
        public async Task<IActionResult> GetEntityAddresses([FromBody] GetEntityAddressesRequest getAddressesRequest)

         => Ok(await _addressesService.GetEntityAddresses(getAddressesRequest).ConfigureAwait(true));

        [HttpPost]
        [Route("update-entity-addresses")]
        public async Task<IActionResult> UpdateEntityAddress([FromBody] UpdateEntityAddressRequest updateEntityAddressRequest)

         => Ok(await _addressesService.UpdateEntityAddress(updateEntityAddressRequest).ConfigureAwait(true));

        [HttpDelete]
        [Route("delete-entity-addresses")]
        public async Task<IActionResult> DeleteEntityAddress([FromForm, Required] Guid entityAddressId)

         => Ok(await _addressesService.DeleteEntityAddress(entityAddressId).ConfigureAwait(true));

    }
    }
