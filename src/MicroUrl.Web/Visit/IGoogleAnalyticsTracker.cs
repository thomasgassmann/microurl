namespace MicroUrl.Visit
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IGoogleAnalyticsTracker
    {
        Task TrackAsync(string key, HttpContext context);
    }
}