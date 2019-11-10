namespace MicroUrl.Web.Urls
{
    using System.Threading.Tasks;

    public interface IUrlService
    {
        Task<string> CreateAsync(string url, string key = null);
    }
}