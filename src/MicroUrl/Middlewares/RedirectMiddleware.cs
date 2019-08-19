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
            var path = context.Request.Path.Value?.TrimStart('/');
            var redirectUrl = !string.IsNullOrEmpty(path) ? await urlService.GetRedirectUrl(path) : null;
            if (redirectUrl != null)
            {
                context.Response.Redirect(redirectUrl);   
            }
            else
            {
                await _next(context);
            }
        }
    }
}