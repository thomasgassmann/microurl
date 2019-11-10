namespace MicroUrl.Web.Redirects
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using MicroUrl.Storage.Dto;

    public interface IRedirectService
    {
        Task<string> GetRedirectUrlAndTrackAsync(string key, HttpContext context);

        string ComputeTargetUrl(Redirectable redirectable);
    }
}