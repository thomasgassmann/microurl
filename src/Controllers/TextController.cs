namespace MicroUrl.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using MicroUrl.Controllers.Extensions;
    using MicroUrl.Controllers.Models;
    using MicroUrl.Text;

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
            var result = await _textService.SaveAsync(createTextModel.Language, createTextModel.Content);
            return this.CreatedUrl(result);
        }
    }
}