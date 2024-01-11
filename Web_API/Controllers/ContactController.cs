using BAL.Interfaces;
using BAL.Request;
using BAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private IContactService _contactService;
        private IConfiguration _config;
        private readonly ILogger<ContactController> _logger;
        public ContactController(IContactService contactService, IConfiguration config, ILogger<ContactController> logger)
        {
            _contactService = contactService;
            _config = config;
            _logger = logger;

        }

        [HttpPost]
        [Route("create-entity-contact")]
        public async Task<IActionResult> CreateEntityContact([FromBody] CreateEntityContactsRequest createEntityContactRequest)

         => Ok(await _contactService.CreateEntityContact(createEntityContactRequest).ConfigureAwait(true));

        [HttpPost]
        [Route("get-entity-contact")]
        public async Task<IActionResult> GetEntityContact([FromBody] GetEntityAddressesRequest getContactRequest)

       => Ok(await _contactService.GetEntityContact(getContactRequest).ConfigureAwait(true));

        [HttpPost]
        [Route("update-entity-contact")]
        public async Task<IActionResult> UpdateEntityContact([FromBody] UpdateEntiyContactRequest updateReques)
      => Ok(await _contactService.UpdateEntityContact(updateReques).ConfigureAwait(true));

    }
}
