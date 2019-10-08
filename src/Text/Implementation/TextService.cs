namespace MicroUrl.Text.Implementation
{
    using System;
    using System.Threading.Tasks;
    using Google.Protobuf.WellKnownTypes;
    using MicroUrl.Storage;
    using MicroUrl.Storage.Entities;
    using MicroUrl.Urls;

    public class TextService : ITextService
    {
        private readonly ITextStorageService _textStorageService;
        private readonly IUrlStorageService _urlStorageService;
        private readonly IMicroUrlKeyGenerator _microUrlKeyGenerator;

        public TextService(
            ITextStorageService textStorageService,
            IMicroUrlKeyGenerator microUrlKeyGenerator,
            IUrlStorageService urlStorageService)
        {
            _textStorageService = textStorageService;
            _microUrlKeyGenerator = microUrlKeyGenerator;
            _urlStorageService = urlStorageService;
        }
        
        public async Task<string> SaveAsync(string language, string content)
        {
            return await _textStorageService.CreateAsync(new MicroUrlTextEntity
            {
                Created = Timestamp.FromDateTime(DateTime.UtcNow),
                Enabled = true,
                Key = await _microUrlKeyGenerator.GenerateKeyAsync(),
                Language = language,
                Text = content
            });
        }

        public async Task<string> GetRawContentAsync(string key)
        {
            var result = await _textStorageService.LoadAsync(key);
            return result?.Key ?? (await _urlStorageService.LoadAsync(key))?.Url;
        }
    }
}