namespace MicroUrl.Storage.Stores.Implementation
{
    using System.Threading.Tasks;
    using MicroUrl.Storage.Dto;

    public class MicroTextStore : IMicroTextStore
    {
        private readonly IRedirectableStore _redirectableStore;

        public MicroTextStore(IRedirectableStore redirectableStore)
        {
            _redirectableStore = redirectableStore;
        }

        public async Task<string> SaveAsync(MicroText microText) =>
            await _redirectableStore.SaveAsync(microText);

        public async Task<MicroText> LoadAsync(string key)
        {
            var result = await _redirectableStore.LoadAsync(key);
            if (result is MicroText text)
            {
                return text;
            }

            return null;
        }
    }
}