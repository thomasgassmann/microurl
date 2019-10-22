namespace MicroUrl.Markdown.Implementation
{
    using MicroUrl.Storage;
    using System.Threading.Tasks;

    public class MarkdownService : IMarkdownService
    {
        private readonly ITextStorageService _textStorageService;

        public const string Markdown = "markdown";

        public MarkdownService(ITextStorageService textStorageService)
        {
            _textStorageService = textStorageService;
        }

        public async Task<string> GetMarkdownStringAsync(string key)
        {
            var entity = await _textStorageService.LoadAsync(key);
            if (entity == null || entity.Language != Markdown)
            {
                return null;
            }

            
            var processor = new HeyRed.MarkdownSharp.Markdown();
            return processor.Transform(entity.Text);
        }
    }
}
