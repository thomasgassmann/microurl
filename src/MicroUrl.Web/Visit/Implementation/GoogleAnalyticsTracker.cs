namespace MicroUrl.Web.Visit.Implementation
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using MicroUrl.Common;

    public class GoogleAnalyticsTracker : IGoogleAnalyticsTracker
    {
        private readonly IEnvConfigurationStore _configurationStore;
        private readonly ILogger<GoogleAnalyticsTracker> _logger;

        public GoogleAnalyticsTracker(IEnvConfigurationStore configurationStore, ILogger<GoogleAnalyticsTracker> logger)
        {
            _configurationStore = configurationStore;
            _logger = logger;
        }

        public async Task TrackAsync(string key, HttpContext context)
        {
            var analyticsId = _configurationStore.GetMicroUrlSettings().AnalyticsId;
            if (string.IsNullOrEmpty(analyticsId))
            {
                return;
            }

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

            using var client = new HttpClient { BaseAddress = new Uri("https://www.google-analytics.com") };
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
                $"/collect?v=1&t=pageview&tid={analyticsId}&cid={connectionId}&dh={host}&dp={key}";
            var response = await client.GetAsync(trackingUrl);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ArgumentException();
            }
        }
    }
}