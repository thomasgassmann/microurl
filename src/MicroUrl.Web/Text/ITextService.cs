namespace MicroUrl.Web.Text
{
    using System.Threading.Tasks;

    public interface ITextService
    {
        Task<string> SaveAsync(string language, string content);

        Task<TextWithLanguage> LoadAsync(string key);

        Task<TextWithLanguageDiff> LoadDiffAsync(string key, string diffKey);
    }
}