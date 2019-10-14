namespace MicroUrl.Text
{
    using System.Threading.Tasks;

    public interface ITextService
    {
        Task<string> SaveAsync(string language, string content);
    }
}