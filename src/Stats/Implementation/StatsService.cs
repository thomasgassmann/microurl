namespace MicroUrl.Stats.Implementation
{
    using System;
    using System.Collections.Generic;
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

        public async Task<MicroUrlStats> GetStats(string key)
        {
            var microUrl = await _urlStorageService.LoadAsync(key);
            if (microUrl == null)
            {
                return null;
            }

//            var hits = new Dictionary<string, long>();
//            var queryResult = _visitorStorageService.GetVisitorCountAsync(
//                key,
//                microUrl.Created.ToDateTime(),
//                DateTime.UtcNow);
//            await foreach (var item in queryResult)
//            {
//                
//            }
            
            return new MicroUrlStats
            {
                Key = key,
                TargetUrl = microUrl.Url
            };
        }
    }
}