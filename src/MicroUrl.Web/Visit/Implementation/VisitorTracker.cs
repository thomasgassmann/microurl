namespace MicroUrl.Web.Visit.Implementation
{
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using MicroUrl.Web.Storage.Dto;
    using MicroUrl.Web.Storage.Stores;

    public class VisitorTracker : IVisitorTracker
    {
        private readonly IVisitStore _visitStore;

        private readonly IGoogleAnalyticsTracker _googleAnalyticsTracker;
        
        public VisitorTracker(
            IVisitStore visitStore,
            IGoogleAnalyticsTracker googleAnalyticsTracker)
        {
            _visitStore = visitStore;
            _googleAnalyticsTracker = googleAnalyticsTracker;
        }
        
        public async Task SaveVisitAsync(string key, HttpContext context)
        {
            var gaTask = TrackGoogleAnalytics(key, context);
            var storageTask = _visitStore.CreateAsync(new Visit
            {
                Headers = GetHeaders(context),
                Ip = GetIpAddress(context),
                Key = key
            });
            await Task.WhenAll(gaTask, storageTask);
        }

        private string GetIpAddress(HttpContext context) =>
            context.Connection.RemoteIpAddress.ToString();

        private async Task TrackGoogleAnalytics(string key, HttpContext context)
        {
            await _googleAnalyticsTracker.TrackAsync(key, context);
        }

        private static string GetHeaders(HttpContext context)
        {
            var stringBuilder = new StringBuilder();
            foreach (var (key, value) in context.Request.Headers)
            {
                stringBuilder.AppendLine($"{key}: {value}");
            }

            return stringBuilder.ToString();
        }
    }
}