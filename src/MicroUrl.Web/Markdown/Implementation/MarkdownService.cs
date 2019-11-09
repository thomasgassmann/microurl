namespace MicroUrl.Markdown.Implementation
{
    using System.Threading.Tasks;
    using MicroUrl.Storage.Abstractions;

    public class MarkdownService : IMarkdownService
    {
        private readonly IStorageFactory _storageFactory;

        public const string Markdown = "markdown";

        public MarkdownService(IStorageFactory storageFactory)
        {
            _storageFactory = storageFactory;
        }

        public async Task<string> GetMarkdownStringAsync(string key)
        {
            var entity = await .LoadAsync(key);
            if (entity == null || entity.Language != Markdown)
            {
                return null;
            }

            var processor = new HeyRed.MarkdownSharp.Markdown();
            return processor.Transform(entity.Text);
        }
    }
}
