namespace MicroUrl.Visit.Implementation
{
    using System.Text;
    using System.Threading.Tasks;
    using Google.Cloud.Datastore.V1;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using MicroUrl.Infrastructure.Settings;
    using MicroUrl.Storage;
    using MicroUrl.Urls;

    public class VisitorTracker : IVisitorTracker
    {
        private readonly IStorageFactory _storageFactory;

        private readonly IGoogleAnalyticsTracker _googleAnalyticsTracker;

        private readonly IOptions<MicroUrlSettings> _options;
        
        private const string Kind = "Visit";
        
        public VisitorTracker(
            IStorageFactory storageFactory,
            IOptions<MicroUrlSettings> options,
            IGoogleAnalyticsTracker googleAnalyticsTracker)
        {
            _storageFactory = storageFactory;
            _options = options;
            _googleAnalyticsTracker = googleAnalyticsTracker;
        }
        
        public async Task SaveVisitAsync(MicroUrlEntity entity, HttpContext context)
        {
            var storage = _storageFactory.GetStorage();
            await TrackGoogleAnalytics(entity, context);
            await storage.InsertAsync(new Entity
            {
                Key = storage.CreateKeyFactory(Kind).CreateIncompleteKey(),
                Properties =
                {
                    { "key", entity.Key },
                    { "headers", GetHeaders(context) },
                    { "ip", GetIpAddress(context) }
                }
            });
        }

        private string GetIpAddress(HttpContext context) =>
            context.Connection.RemoteIpAddress.ToString();

        private async Task TrackGoogleAnalytics(MicroUrlEntity entity, HttpContext context)
        {
            await _googleAnalyticsTracker.TrackAsync(entity, context);
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