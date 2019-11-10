namespace MicroUrl.Raw.Implementation
{
    using System.Threading.Tasks;
    using MicroUrl.Storage.Dto;
    using MicroUrl.Storage.Stores;

    public class RawService : IRawService
    {
        private readonly IRedirectableStore _redirectableStore;

        public RawService(IRedirectableStore redirectableStore)
        {
            _redirectableStore = redirectableStore;
        }

        public async Task<string> GetRawContentAsync(string key)
        {
            var redirectable = await _redirectableStore.LoadAsync(key);
            return redirectable switch
            {
                MicroText text => text.Text,
                MicroUrl url => url.Url,
                _ => null
            };
        }
    }
}
