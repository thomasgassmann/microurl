namespace MicroUrl.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using MicroUrl.Web.Controllers.Extensions;
    using MicroUrl.Web.Controllers.Models;
    using MicroUrl.Web.Text;

    [Route("api/microtext")]
    [ApiController]
    public class TextController : Controller
    {
        private readonly ITextService _textService;

        public TextController(ITextService textService)
        {
            _textService = textService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTextModel createTextModel)
        {
            var result = await _textService.CreateAsync(createTextModel.Language, createTextModel.Content);
            return this.CreatedUrl(result);
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetAsync(string key)
        {
            var result = await _textService.LoadAsync(key);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{key}/diff/{diffKey}")]
        public async Task<IActionResult> DiffAsync(string key, string diffKey)
        {
            var result = await _textService.LoadDiffAsync(key, diffKey);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}