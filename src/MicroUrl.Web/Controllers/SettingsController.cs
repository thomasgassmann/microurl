namespace MicroUrl.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MicroUrl.Common;

    [ApiController]
    [Route("settings")]
    public class SettingsController : Controller
    {
        private readonly IConfigurationStore _configurationStore;

        public SettingsController(IConfigurationStore configurationStore) =>
            _configurationStore = configurationStore;

        [HttpGet]
        public IActionResult Get() =>
            Ok(_configurationStore.GetMicroUrlSettings());
    }
}