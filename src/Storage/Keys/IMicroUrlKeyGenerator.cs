namespace MicroUrl.Urls
{
    using System.Threading.Tasks;

    public interface IMicroUrlKeyGenerator
    {
        Task<string> GenerateKeyAsync(string customKey = null);
    }
}