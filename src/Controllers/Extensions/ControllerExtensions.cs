namespace MicroUrl.Controllers.Extensions
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtensions
    {
        public static IActionResult CreatedUrl(this Controller controller, string key)
        {
            return new JsonResult(new {Key = key}) {StatusCode = (int) HttpStatusCode.Created};
        }
    }
}