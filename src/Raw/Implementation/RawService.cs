namespace MicroUrl.Raw.Implementation
{
    using MicroUrl.Storage;
    using System.Threading.Tasks;

    public class RawService : IRawService
    {
        private readonly IUrlStorageService _urlStorageService;
        private readonly ITextStorageService _textStorageService;

        public RawService(IUrlStorageService urlStorageService, ITextStorageService textStorageService)
        {
            _urlStorageService = urlStorageService;
            _textStorageService = textStorageService;
        }

        public async Task<string> GetRawContentAsync(string key)
        {
            var result = await _textStorageService.LoadAsync(key);
            return result?.Text ?? (await _urlStorageService.LoadAsync(key))?.Url;
        }
    }
}
