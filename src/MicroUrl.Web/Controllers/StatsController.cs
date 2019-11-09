namespace MicroUrl.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using MicroUrl.Stats;

    [Route("api/microurl")]
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