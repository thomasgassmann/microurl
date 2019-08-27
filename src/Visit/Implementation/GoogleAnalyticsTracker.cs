namespace MicroUrl.Visit.Implementation
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MicroUrl.Infrastructure.Settings;
    using MicroUrl.Urls;

    public class GoogleAnalyticsTracker : IGoogleAnalyticsTracker
    {
        private readonly IOptions<MicroUrlSettings> _options;
        private readonly ILogger<GoogleAnalyticsTracker> _logger;
        
        public GoogleAnalyticsTracker(IOptions<MicroUrlSettings> options, ILogger<GoogleAnalyticsTracker> logger)
        {
            _options = options;
            _logger = logger;
        }
        
        public async Task TrackAsync(MicroUrlEntity entity, HttpContext context)
        {
            var blockedHeaderList = new[]
            {
                "Connection",
                "DNT",
                "Upgrade-Insecure-Requests",
                "Host",
                "Content-Length"
            };
            
            using var client = new HttpClient {BaseAddress = new Uri("https://www.google-analytics.com")};
            foreach (var (key, value) in context.Request.Headers)
            {
                if (!blockedHeaderList.Contains(key))
                {
                    try
                    {
                        client.DefaultRequestHeaders.Add(key, value.ToArray());
                    }
                    catch (InvalidOperationException)
                    {
                        _logger.LogError($"{key} is not a valid header value (set manually)!");
                    }
                }
            }
            
            var host = context.Request.Host.Host;
            var connectionId = context.Connection.Id;
            var urlEncoded = WebUtility.UrlEncode(entity.Url);
            var trackingUrl =
                $"/collect?v=1&t=pageview&tid={_options.Value.AnalyticsId}&cid={connectionId}&dh={host}&dp={entity.Key}&dt={urlEncoded}";
            var response = await client.GetAsync(trackingUrl);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ArgumentException();
            }
        }
    }
}