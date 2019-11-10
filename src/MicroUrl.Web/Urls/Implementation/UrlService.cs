namespace MicroUrl.Web.Urls.Implementation
{
    using System;
    using System.Threading.Tasks;
    using Google.Protobuf.WellKnownTypes;
    using Microsoft.AspNetCore.Http;
    using MicroUrl.Web.Storage;
    using MicroUrl.Web.Storage.Dto;
    using MicroUrl.Web.Storage.Entities;
    using MicroUrl.Web.Storage.Stores;
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