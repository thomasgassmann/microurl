namespace MicroUrl.Web.Visit.Implementation
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MicroUrl.Web.Infrastructure.Settings;

    public class GoogleAnalyticsTracker : IGoogleAnalyticsTracker
    {
        private readonly IOptions<MicroUrlSettings> _options;
        private readonly ILogger<GoogleAnalyticsTracker> _logger;
        
        public GoogleAnalyticsTracker(IOptions<MicroUrlSettings> options, ILogger<GoogleAnalyticsTracker> logger)
        {
            _options = options;
            _logger = logger;
        }
        
        public async Task TrackAsync(string key, HttpContext context)
        {
            // TODO: this solution seems quite ugly
            var blockedHeaderList = new[]
            {
                "Connection",
                "DNT",
                "Upgrade-Insecure-Requests",
                "Host",
                "Content-Length"
            };
            
            // TODO: x-cloud-trace-context, x-forwarded-for
            
            using var client = new HttpClient {BaseAddress = new Uri("https://www.google-analytics.com")};
            foreach (var (headerKey, headerValue) in context.Request.Headers)
            {
                if (blockedHeaderList.Contains(headerKey))
                {
                    continue;
                }

                try
                {
                    client.DefaultRequestHeaders.Add(headerKey, headerValue.ToArray());
                }
                catch (InvalidOperationException)
                {
                    _logger.LogError($"{key} is not a valid header value (set manually)!");
                }
            }
            
            var host = context.Request.Host.Host;
            var connectionId = context.Connection.Id;
            var trackingUrl =
                $"/collect?v=1&t=pageview&tid={_options.Value.AnalyticsId}&cid={connectionId}&dh={host}&dp={key}";
            var response = await client.GetAsync(trackingUrl);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ArgumentException();
            }
        }
    }
}