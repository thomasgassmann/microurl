namespace MicroUrl.Urls.Implementation
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Google.Protobuf.WellKnownTypes;
    using Microsoft.AspNetCore.Http;
    using MicroUrl.Visit;

    public class UrlService : IUrlService
    {
        private const string Characters = "abcdefghiklmnopqrstuvwxyz0123456789";
        
        private readonly IUrlStorageService _storageService;
        private readonly IVisitorTracker _visitorTracker;
        
        private readonly Random _random = new Random();

        public UrlService(IUrlStorageService storageService, IVisitorTracker visitorTracker)
        {
            _storageService = storageService;
            _visitorTracker = visitorTracker;
        }

        public async Task<string> SaveAsync(string url, string key = null)
        {
            if (key != null && await _storageService.ExistsAsync(key))
            {
                throw new ExistingKeyException(key);
            }
            
            var createdKey = await _storageService.SaveAsync(new MicroUrlEntity
            {
                Created = Timestamp.FromDateTime(DateTime.UtcNow),
                Enabled = true,
                Url = url,
                Key = key ?? await GenerateKey()
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
            if (!microUrl.Enabled)
            {
                return null;
            }
            
            return microUrl.Url;
        }

        private async Task<string> GenerateKey()
        {
            for (var i = 1;; i++)
            {
                var key = GenerateKeyOfLength(i);
                if (!await _storageService.ExistsAsync(key))
                {
                    return key;
                }
            }
        }

        private string GenerateKeyOfLength(int length)
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                stringBuilder.Append(Characters[_random.Next(0, Characters.Length - 1)]);
            }

            return stringBuilder.ToString();
        }
    }
}