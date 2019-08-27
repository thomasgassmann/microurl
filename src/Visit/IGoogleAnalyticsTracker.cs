namespace MicroUrl.Visit
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using MicroUrl.Urls;

    public interface IGoogleAnalyticsTracker
    {
        Task TrackAsync(MicroUrlEntity entity, HttpContext context);
    }
}