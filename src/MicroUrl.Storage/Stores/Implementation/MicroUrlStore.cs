namespace MicroUrl.Storage.Stores.Implementation
{
    using System.Threading.Tasks;
    using MicroUrl.Storage.Dto;

    public class MicroUrlStore : IMicroUrlStore
    {
        private readonly IRedirectableStore _redirectableStore;

        public MicroUrlStore(IRedirectableStore redirectableStore)
        {
            _redirectableStore = redirectableStore;
        }

        public async Task<string> CreateAsync(MicroUrl microUrl) =>
            await _redirectableStore.CreateAsync(microUrl);

        public async Task<MicroUrl> LoadAsync(string key)
        {
            var result = await _redirectableStore.LoadAsync(key);
            if (result is MicroUrl url)
            {
                return url;
            }

            return null;
        }
    }
}