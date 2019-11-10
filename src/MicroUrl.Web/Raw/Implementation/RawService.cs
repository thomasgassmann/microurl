namespace MicroUrl.Web.Raw.Implementation
{
    using System.Threading.Tasks;
    using MicroUrl.Web.Storage.Dto;
    using MicroUrl.Web.Storage.Stores;

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
