namespace MicroUrl.Urls
{
    using System.Threading.Tasks;

    public interface IUrlService
    {
        Task<string> SaveAsync(string url);
    }
}