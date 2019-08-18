namespace MicroUrl.Middlewares
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class RedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
        }
    }
}