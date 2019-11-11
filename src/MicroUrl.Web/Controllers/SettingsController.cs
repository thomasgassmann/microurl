namespace MicroUrl.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using MicroUrl.Web.Infrastructure.Settings;

    [ApiController]
    [Route("api/settings")]
    public class SettingsController : Controller
    {
        private readonly IOptions<MicroUrlSettings> _options;
        
        public SettingsController(IOptions<MicroUrlSettings> options)
        {
            _options = options;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_options.Value);
        }
    }
}