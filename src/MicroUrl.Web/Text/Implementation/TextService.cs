namespace MicroUrl.Web.Text.Implementation
{
    using System.Threading.Tasks;
    using MicroUrl.Storage.Dto;
    using MicroUrl.Storage.Stores;
    using MicroUrl.Web.Keys;

    public class TextService : ITextService
    {
        private readonly IMicroTextStore _microTextStore;
        private readonly IMicroUrlKeyGenerator _keyGenerator;

        public TextService(
            IMicroTextStore microTextStore,
            IMicroUrlKeyGenerator keyGenerator)
        {
            _microTextStore = microTextStore;
            _keyGenerator = keyGenerator;
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

        public async Task<string> CreateAsync(string language, string content)
        {
            return await _microTextStore.CreateAsync(new MicroText
            {
                Key = await _keyGenerator.GenerateKeyAsync(),
                Enabled = true,
                Language = language,
                Text = content
            });
        }

        private async Task<TextWithLanguage> LoadSingleTextWithLanguageAsync(string key)
        {
            var result = await _microTextStore.LoadAsync(key);
            if (result == null || !result.Enabled)
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