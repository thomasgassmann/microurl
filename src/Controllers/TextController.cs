namespace MicroUrl.Controllers
{
    using Microsoft.AspNetCore.Mvc;
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
        public IActionResult CreateAsync([FromBody] CreateTextModel createTextModel)
        {
            return Ok();
        }
    }
}