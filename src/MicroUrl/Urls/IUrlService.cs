namespace MicroUrl.Urls
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IUrlService
    {
        Task<string> SaveAsync(string url);

        Task<string> GetRedirectUrlAndTrackAsync(string key, HttpContext context);
    }
}