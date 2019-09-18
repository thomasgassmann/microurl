namespace MicroUrl.Urls.Implementation
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Google.Protobuf.WellKnownTypes;
    using Microsoft.AspNetCore.Http;
    using MicroUrl.Storage.Entities;
    using MicroUrl.Visit;

    public class UrlService : IUrlService
    {
        private readonly IUrlStorageService _storageService;
        private readonly IVisitorTracker _visitorTracker;
        private readonly IMicroUrlKeyGenerator _microUrlKeyGenerator;
        
        private readonly Random _random = new Random();

        public UrlService(IUrlStorageService storageService, IVisitorTracker visitorTracker, IMicroUrlKeyGenerator microUrlKeyGenerator)
        {
            _storageService = storageService;
            _visitorTracker = visitorTracker;
            _microUrlKeyGenerator = microUrlKeyGenerator;
        }

        public async Task<string> SaveAsync(string url, string key = null)
        {
            var generatedKey = await _microUrlKeyGenerator.GenerateKeyAsync(key);
            var createdKey = await _storageService.SaveAsync(new MicroUrlEntity
            {
                Created = Timestamp.FromDateTime(DateTime.UtcNow),
                Enabled = true,
                Url = url,
                Key = generatedKey
            });
            return createdKey;
        }

        public async Task<string> GetRedirectUrlAndTrackAsync(string key, HttpContext context)
        {
            var microUrl = await _storageService.LoadAsync(key);
            if (microUrl == null)
            {
                return null;
            }

            await _visitorTracker.SaveVisitAsync(microUrl, context);           
            return !microUrl.Enabled ? null : microUrl.Url;
        }
    }
}