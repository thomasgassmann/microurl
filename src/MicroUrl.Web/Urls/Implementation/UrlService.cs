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
        
        public UrlService(IMicroUrlStore microUrlStore)
        {
            _microUrlStore = microUrlStore;
        }

        public Task<string> CreateAsync(string url, string key = null) =>
            _microUrlStore.CreateAsync(new MicroUrl
            {
                Key = key,
                Enabled = true,
                Url = url
            });
    }
}