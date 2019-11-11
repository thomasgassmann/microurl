namespace MicroUrl.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MicroUrl.Common;

    [ApiController]
    [Route("settings")]
    public class SettingsController : Controller
    {
        private readonly IEnvConfigurationStore _configurationStore;

        public SettingsController(IEnvConfigurationStore configurationStore) =>
            _configurationStore = configurationStore;

        [HttpGet]
        public IActionResult Get() =>
            Ok(_configurationStore.GetMicroUrlSettings());
    }
}