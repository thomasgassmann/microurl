namespace MicroUrl.Web.Urls.Implementation
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using MicroUrl.Storage.Dto;
    using MicroUrl.Storage.Stores;
    using MicroUrl.Web.Visit;

    public class UrlService : IUrlService
    {
        private readonly IMicroUrlStore _microUrlStore;
        private readonly IVisitorTracker _visitorTracker;
        
        public UrlService(IMicroUrlStore microUrlStore, IVisitorTracker visitorTracker)
        {
            _microUrlStore = microUrlStore;
            _visitorTracker = visitorTracker;
        }

        public Task<string> CreateAsync(string url, string key = null) =>
            _microUrlStore.CreateAsync(new MicroUrl
            {
                Key = key,
                Enabled = true,
                Url = url
            });

        public async Task<string> GetRedirectUrlAndTrackAsync(string key, HttpContext context)
        {
            var microUrl = await _microUrlStore.LoadAsync(key);
            if (microUrl == null)
            {
                return null;
            }

            await _visitorTracker.SaveVisitAsync(key, context);           
            return !microUrl.Enabled ? null : microUrl.Url;
        }
    }
}