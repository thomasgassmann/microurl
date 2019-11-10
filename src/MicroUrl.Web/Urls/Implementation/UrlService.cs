namespace MicroUrl.Web.Urls.Implementation
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using MicroUrl.Storage;
    using MicroUrl.Storage.Dto;
    using MicroUrl.Storage.Stores;
    using MicroUrl.Web.Visit;

    public class UrlService : IUrlService
    {
        private readonly IMicroUrlStore _microUrlStore;
        private readonly IVisitorTracker _visitorTracker;
        private readonly IMicroUrlKeyGenerator _microUrlKeyGenerator;
        
        public UrlService(IMicroUrlStore microUrlStore, IVisitorTracker visitorTracker, IMicroUrlKeyGenerator microUrlKeyGenerator)
        {
            _microUrlStore = microUrlStore;
            _visitorTracker = visitorTracker;
            _microUrlKeyGenerator = microUrlKeyGenerator;
        }

        public async Task<string> SaveAsync(string url, string key = null)
        {
            var createdKey = await _microUrlStore.SaveAsync(new MicroUrl
            {
                Enabled = true,
                Url = url
            });
            return createdKey;
        }

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