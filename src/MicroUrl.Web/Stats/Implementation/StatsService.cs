namespace MicroUrl.Web.Stats.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MicroUrl.Storage.Dto;
    using MicroUrl.Storage.Stores;
    using MicroUrl.Web.Redirects;

    internal class StatCount
    {
        internal long Visitors { get; set; }

        internal long UniqueVisitors { get; set; }

        internal HashSet<string> SeenVisitors { get; set; }

        internal Func<Visit, bool> VisitApplies { get; set; }

        internal DateTime? Date { get; set; }
    }

    public class StatsService : IStatsService
    {
        private const int LastXDays = 7;

        private readonly IRedirectableStore _redirectableStore;
        private readonly IVisitStore _visitStore;
        private readonly IRedirectService _redirectService;

        public StatsService(
            IRedirectableStore redirectableStore,
            IVisitStore visitStore,
            IRedirectService redirectService)
        {
            _redirectableStore = redirectableStore;
            _visitStore = visitStore;
            _redirectService = redirectService;
        }

        public async Task<MicroUrlStats> GetStatsAsync(string key)
        {
            var redirectable = await _redirectableStore.LoadAsync(key);
            if (redirectable == null)
            {
                return null;
            }

            var from = redirectable.Created;
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
                    VisitApplies = item => item.Created.Date == x,
                    Date = x
                })
                .ToList();
            var stats = new List<StatCount> { allTimeStats };
            dayStats.ForEach(stats.Add);
            
            await foreach (var item in _visitStore.GetVisitsOfRediretableBetween(key, from, to))
            {
                foreach (var stat in stats.Where(stat => stat.VisitApplies(item)))
                {
                    stat.Visitors++;
                    if (!stat.SeenVisitors.Contains(item.Ip))
                    {
                        stat.UniqueVisitors++;
                        stat.SeenVisitors.Add(item.Ip);
                    }
                }
            }

            return new MicroUrlStats
            {
                Key = key,
                TargetUrl = _redirectService.ComputeTargetUrl(redirectable),
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