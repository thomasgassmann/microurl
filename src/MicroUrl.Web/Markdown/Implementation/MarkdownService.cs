namespace MicroUrl.Web.Markdown.Implementation
{
    using System.Threading.Tasks;
    using MicroUrl.Storage.Stores;

    public class MarkdownService : IMarkdownService
    {
        private readonly IMicroTextStore _microTextStore;

        private const string Markdown = "markdown";

        public MarkdownService(IMicroTextStore microTextStore)
        {
            _microTextStore = microTextStore;
        }

        public async Task<string> GetMarkdownStringAsync(string key)
        {
            var entity = await _microTextStore.LoadAsync(key);
            if (entity == null || entity.Language != Markdown)
            {
                return null;
            }

            var processor = new HeyRed.MarkdownSharp.Markdown();
            return processor.Transform(entity.Text);
        }
    }
}
