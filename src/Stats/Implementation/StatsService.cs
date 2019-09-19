namespace MicroUrl.Stats.Implementation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using MicroUrl.Storage;
    using MicroUrl.Visit;

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
            // TODO: use IAsyncEnumerable
            var queryResult = await _visitStorageService.GetVisitorCountAsync(key, from, to);

            return new MicroUrlStats
            {
                Key = key,
                TargetUrl = microUrl.Url,
                AllTime = new HitStats
                {
                    From = from,
                    To = to,
                    Visitors = queryResult.LongCount(),
                    UniqueVisitors = queryResult.GroupBy(x => x.Ip).LongCount()
                },
                Recents = Enumerable.Range(0, 7).Select(x => DateTime.UtcNow.Date.AddDays(-x))
                    .Select(day =>
                    {
                        var dayEnd = day.AddDays(1);
                        var queriesOnDay = queryResult.Where(x => 
                            x.Created.ToDateTime() >= day && 
                            x.Created.ToDateTime() <= dayEnd);
                        return new HitStats
                        {
                            From = day,
                            To = dayEnd,
                            Visitors = queriesOnDay.LongCount(),
                            UniqueVisitors = queriesOnDay.GroupBy(x => x.Ip).LongCount()
                        };
                    }).ToArray()
            };
        }
    }
}