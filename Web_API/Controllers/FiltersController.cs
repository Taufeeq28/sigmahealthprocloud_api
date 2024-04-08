using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiltersController : ControllerBase
    {
        private readonly SigmaproIisContext _context;
          public FiltersController(SigmaproIisContext context)
        {
            _context = context;
        }

    }
}
