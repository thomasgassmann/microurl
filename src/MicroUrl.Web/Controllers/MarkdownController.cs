namespace MicroUrl.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using MicroUrl.Web.Markdown;

    [Route("md")]
    [ApiController]
    public class MarkdownController : Controller
    {
        private readonly IMarkdownService _markdownService;

        public MarkdownController(IMarkdownService markdownService)
        {
            _markdownService = markdownService;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetMarkdownAsync(string key)
        {
            var markdown = await _markdownService.GetMarkdownStringAsync(key);
            if (markdown == null)
            {
                return NotFound();
            }

            return Content(markdown, "text/html");
        }
    }
}
