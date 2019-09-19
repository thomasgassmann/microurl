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
        private readonly IMicroUrlKeyGenerator _microUrlKeyGenerator;

        public TextService(ITextStorageService textStorageService, IMicroUrlKeyGenerator microUrlKeyGenerator)
        {
            _textStorageService = textStorageService;
            _microUrlKeyGenerator = microUrlKeyGenerator;
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
            return result?.Text;
        }
    }
}