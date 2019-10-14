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

        public TextService(
            ITextStorageService textStorageService,
            IMicroUrlKeyGenerator microUrlKeyGenerator)
        {
            _textStorageService = textStorageService;
            _microUrlKeyGenerator = microUrlKeyGenerator;
        }

        public async Task<TextWithLanguage> LoadAsync(string key) =>
            await LoadSingleTextWithLanguageAsync(key);

        public async Task<TextWithLanguageDiff> LoadDiffAsync(string key, string diffKey)
        {
            var diffItems = await Task.WhenAll(new[]
            {
                LoadSingleTextWithLanguageAsync(key),
                LoadSingleTextWithLanguageAsync(diffKey)
            });
            if (diffItems[0] == null || diffItems[1] == null)
            {
                return null;
            }

            return new TextWithLanguageDiff
            {
                Left = diffItems[0],
                Right = diffItems[1]
            };
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

        private async Task<TextWithLanguage> LoadSingleTextWithLanguageAsync(string key)
        {
            var result = await _textStorageService.LoadAsync(key);
            if (!result.Enabled)
            {
                return null;
            }

            return new TextWithLanguage
            {
                Content = result.Text,
                Language = result.Language
            };
        }
    }
}