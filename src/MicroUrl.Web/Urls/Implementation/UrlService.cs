namespace MicroUrl.Web.Urls.Implementation
{
    using System.Threading.Tasks;
    using MicroUrl.Storage.Dto;
    using MicroUrl.Storage.Stores;
    using MicroUrl.Web.Keys;

    public class UrlService : IUrlService
    {
        private readonly IMicroUrlStore _microUrlStore;
        private readonly IMicroUrlKeyGenerator _microUrlKeyGenerator;
        
        public UrlService(IMicroUrlStore microUrlStore, IMicroUrlKeyGenerator microUrlKeyGenerator)
        {
            _microUrlStore = microUrlStore;
            _microUrlKeyGenerator = microUrlKeyGenerator;
        }

        public async Task<string> CreateAsync(string url, string key = null) =>
            await _microUrlStore.CreateAsync(new MicroUrl
            {
                Key = await _microUrlKeyGenerator.GenerateKeyAsync(key),
                Enabled = true,
                Url = url
            });
    }
}