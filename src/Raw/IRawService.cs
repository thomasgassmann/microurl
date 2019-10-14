namespace MicroUrl.Raw
{
    using System.Threading.Tasks;

    public interface IRawService
    {
        Task<string> GetRawContentAsync(string key);
    }
}
