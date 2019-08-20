namespace MicroUrl.Urls.Visit.Implementation
{
    using System.Text;
    using System.Threading.Tasks;
    using Google.Cloud.Datastore.V1;
    using GoogleAnalyticsTracker.WebAPI2;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using MicroUrl.Infrastructure.Settings;
    using MicroUrl.Storage;

    public class VisitorTracker : IVisitorTracker
    {
        private readonly IStorageFactory _storageFactory;

        private readonly IOptions<MicroUrlSettings> _options;
        
        private const string Kind = "Visit";
        
        public VisitorTracker(IStorageFactory storageFactory, IOptions<MicroUrlSettings> options)
        {
            _storageFactory = storageFactory;
            _options = options;
        }
        
        public async Task SaveVisitAsync(MicroUrlEntity entity, HttpContext context)
        {
            var storage = _storageFactory.GetStorage();
            await TrackGoogleAnalytics(entity);
            await storage.InsertAsync(new Entity
            {
                Key = storage.CreateKeyFactory(Kind).CreateIncompleteKey(),
                Properties =
                {
                    { "headers", GetHeaders(context) },
                    { "ip", GetIpAddress(context) }
                }
            });
        }

        private string GetIpAddress(HttpContext context) =>
            context.Connection.RemoteIpAddress.ToString();

        private async Task TrackGoogleAnalytics(MicroUrlEntity entity)
        {
            using var tracker = new SimpleTracker(_options.Value.AnalyticsId);
            await tracker.TrackPageViewAsync(entity.Url, entity.Key);
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