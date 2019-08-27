namespace MicroUrl.Stats.Implementation
{
    using System.Threading.Tasks;
    using MicroUrl.Urls;

    public class StatsService : IStatsService
    {
        private readonly IUrlStorageService _urlStorageService;
        
        public StatsService(IUrlStorageService urlStorageService)
        {
            _urlStorageService = urlStorageService;
        }

        public async Task<MicroUrlStats> GetStats(string key)
        {
            var microUrl = await _urlStorageService.LoadAsync(key);
            if (microUrl == null)
            {
                return null;
            }
            
            return new MicroUrlStats
            {
                Key = key,
                TargetUrl = microUrl.Url
            };
        }
    }
}