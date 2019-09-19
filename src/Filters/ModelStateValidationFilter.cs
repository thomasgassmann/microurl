namespace MicroUrl.Filters
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ModelStateValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();
        }
    }
}