namespace MicroUrl.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using MicroUrl.Text;

    [ApiController]
    [Route("raw")]
    public class RawController : Controller
    {
        private readonly ITextService _textService;

        public RawController(ITextService textService)
        {
            _textService = textService;
        }
        
        [HttpGet("{key}")]
        public async Task<IActionResult> GetRawAsync(string key)
        {
            var raw = await _textService.GetRawContentAsync(key);
            if (raw == null)
            {
                return NotFound();
            }

            return Content(raw);
        }
    }
}