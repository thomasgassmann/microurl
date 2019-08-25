namespace MicroUrl.Middlewares
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using MicroUrl.Urls;

    public class RedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUrlService urlService)
        {
            if (context.Request.Method != "GET")
            {
                await _next(context);
                return;
            }
            
            var path = context.Request.Path.Value?.TrimStart('/');
            var redirectUrl = !string.IsNullOrEmpty(path) ? await urlService.GetRedirectUrlAndTrackAsync(path, context) : null;
            if (redirectUrl != null)
            {
                context.Response.Redirect(redirectUrl);
                return;
            }
            
            await _next(context);
        }
    }
}