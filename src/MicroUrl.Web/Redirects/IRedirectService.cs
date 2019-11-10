namespace MicroUrl.Web.Redirects
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IRedirectService
    {
        Task<string> GetRedirectUrlAndTrackAsync(string key, HttpContext context);
    }
}