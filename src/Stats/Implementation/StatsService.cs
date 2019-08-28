namespace MicroUrl.Stats.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MicroUrl.Urls;
    using MicroUrl.Visit;

    public class StatsService : IStatsService
    {
        private const int LastXDays = 7;
        
        private readonly IUrlStorageService _urlStorageService;

        private readonly IVisitorStorageService _visitorStorageService;
        
        public StatsService(
            IUrlStorageService urlStorageService,
            IVisitorStorageService visitorStorageService)
        {
            _urlStorageService = urlStorageService;
            _visitorStorageService = visitorStorageService;
        }

        public async Task<MicroUrlStats> GetStatsAsync(string key)
        {
            var microUrl = await _urlStorageService.LoadAsync(key);
            if (microUrl == null)
            {
                return null;
            }

            var hits = new Dictionary<string, long>();
            var from = microUrl.Created.ToDateTime();
            var to = DateTime.UtcNow;
            var queryResult = await _visitorStorageService.GetVisitorCountAsync(key, from, to);
            
            return new MicroUrlStats
            {
                Key = key,
                TargetUrl = microUrl.Url,
                AllTime = new HitStats
                {
                    From = from,
                    To = to,
                    Visitors = queryResult.LongCount(),
                    UniqueVisitors = queryResult.GroupBy(x => x.Ip).Count()
                }
            };
        }
    }
}