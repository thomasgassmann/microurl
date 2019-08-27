namespace MicroUrl.Controllers
{
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
        public IActionResult GetAsync(string key)
        {
            var stats = _statsService.GetStats(key);
            if (stats == null)
            {
                return NotFound();
            }

            return Ok(stats);
        }
    }
}