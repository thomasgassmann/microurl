namespace MicroUrl.Stats.Implementation
{
    using MicroUrl.Storage;
    using MicroUrl.Storage.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class StatCount
    {
        internal long Visitors { get; set; }

        internal long UniqueVisitors { get; set; }

        internal HashSet<string> SeenVisitors { get; set; }

        internal Func<VisitEntity, bool> VisitApplies { get; set; }

        internal DateTime? Date { get; set; }
    }

    public class StatsService : IStatsService
    {
        private const int LastXDays = 7;

        private readonly IUrlStorageService _urlStorageService;
        private readonly IVisitStorageService _visitStorageService;

        public StatsService(
            IUrlStorageService urlStorageService,
            IVisitStorageService visitStorageService)
        {
            _urlStorageService = urlStorageService;
            _visitStorageService = visitStorageService;
        }

        public async Task<MicroUrlStats> GetStatsAsync(string key)
        {
            var microUrl = await _urlStorageService.LoadAsync(key);
            if (microUrl == null)
            {
                return null;
            }

            var from = microUrl.Created.ToDateTime();
            var to = DateTime.UtcNow;

            var allTimeStats = new StatCount
            {
                SeenVisitors = new HashSet<string>(),
                UniqueVisitors = 0,
                VisitApplies = x => true,
                Visitors = 0
            };
            var dayStats = Enumerable.Range(0, LastXDays).Select(x => DateTime.UtcNow.Date.AddDays(-x))
                .Select(x => new StatCount
                {
                    SeenVisitors = new HashSet<string>(),
                    Visitors = 0,
                    UniqueVisitors = 0,
                    VisitApplies = item => item.Created.ToDateTime().Date == x,
                    Date = x
                })
                .ToList();
            var stats = new List<StatCount> { allTimeStats };
            dayStats.ForEach(stats.Add);
            await foreach (var item in _visitStorageService.GetVisitorCountAsync(key, from, to))
            {
                foreach (var stat in stats)
                {
                    if (stat.VisitApplies(item))
                    {
                        stat.Visitors += 1;
                        if (!stat.SeenVisitors.Contains(item.Ip))
                        {
                            stat.UniqueVisitors += 1;
                            stat.SeenVisitors.Add(item.Ip);
                        }
                    }
                }
            }

            return new MicroUrlStats
            {
                Key = key,
                TargetUrl = microUrl.Url,
                AllTime = new HitStats
                {
                    From = from,
                    To = to,
                    Visitors = allTimeStats.Visitors,
                    UniqueVisitors = allTimeStats.UniqueVisitors
                },
                Recents = dayStats
                    .Select(statsForDay =>
                    {
                        var dayEnd = statsForDay.Date.Value.AddDays(1);
                        return new HitStats
                        {
                            From = statsForDay.Date.Value,
                            To = dayEnd,
                            Visitors = statsForDay.Visitors,
                            UniqueVisitors = statsForDay.UniqueVisitors
                        };
                    }).ToList()
            };
        }
    }
}