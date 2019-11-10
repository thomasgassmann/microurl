namespace MicroUrl.Web.Urls
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IUrlService
    {
        Task<string> SaveAsync(string url, string key = null);

        Task<string> GetRedirectUrlAndTrackAsync(string key, HttpContext context);
    }
}