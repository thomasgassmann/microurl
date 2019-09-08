namespace MicroUrl.Urls
{
    using System.Threading.Tasks;

    public interface IKeyGenerator
    {
        Task<string> GetKeyAsync(string customKey = null);
    }
}