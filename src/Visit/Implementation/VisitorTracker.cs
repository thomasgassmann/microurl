namespace MicroUrl.Visit.Implementation
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Google.Cloud.Datastore.V1;
    using Google.Protobuf.WellKnownTypes;
    using Microsoft.AspNetCore.Http;
    using MicroUrl.Urls;

    public class VisitorTracker : IVisitorTracker
    {
        private readonly IVisitorStorageService _visitorStorageService;

        private readonly IGoogleAnalyticsTracker _googleAnalyticsTracker;
        
        public VisitorTracker(
            IVisitorStorageService visitorStorageService,
            IGoogleAnalyticsTracker googleAnalyticsTracker)
        {
            _visitorStorageService = visitorStorageService;
            _googleAnalyticsTracker = googleAnalyticsTracker;
        }
        
        public async Task SaveVisitAsync(string key, HttpContext context)
        {
            var gaTask = TrackGoogleAnalytics(key, context);
            var storageTask = _visitorStorageService.SaveAsync(new UrlVisitEntity
            {
                Created = Timestamp.FromDateTime(DateTime.UtcNow),
                Headers = GetHeaders(context),
                Ip = GetIpAddress(context),
                Key = key
            });
            await Task.WhenAll(new[] {gaTask, storageTask});
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