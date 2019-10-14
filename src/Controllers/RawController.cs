namespace MicroUrl.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MicroUrl.Raw;
    using System.Threading.Tasks;

    [ApiController]
    [Route("raw")]
    public class RawController : Controller
    {
        private readonly IRawService _rawService;

        public RawController(IRawService rawService)
        {
            _rawService = rawService;
        }
        
        [HttpGet("{key}")]
        public async Task<IActionResult> GetRawAsync(string key)
        {
            var raw = await _rawService.GetRawContentAsync(key);
            if (raw == null)
            {
                return NotFound();
            }

            return Content(raw);
        }
    }
}