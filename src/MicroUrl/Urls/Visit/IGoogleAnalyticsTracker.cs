namespace MicroUrl.Urls.Visit
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IGoogleAnalyticsTracker
    {
        Task TrackAsync(MicroUrlEntity entity, HttpContext context);
    }
}