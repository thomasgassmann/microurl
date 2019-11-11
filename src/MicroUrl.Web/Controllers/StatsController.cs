namespace MicroUrl.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using MicroUrl.Web.Stats;

    [Route("api/microurl")]
    [ApiController]
    public class StatsController : Controller
    {
        private readonly IStatsService _statsService;

        public StatsController(IStatsService statsService)
        {
            _statsService = statsService;
        }

        [HttpGet("{key}/stats")]
        public async Task<IActionResult> GetAsync(string key)
        {
            var stats = await _statsService.GetStatsAsync(key);
            if (stats == null)
            {
                return NotFound();
            }

            return Ok(stats);
        }
    }
}