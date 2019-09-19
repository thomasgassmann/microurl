namespace MicroUrl.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("raw")]
    public class RawController : Controller
    {
        [HttpGet("{key}")]
        public IActionResult GetRawAsync(string key)
        {
            return Ok();
        }
    }
}