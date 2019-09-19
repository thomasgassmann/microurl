namespace MicroUrl.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using MicroUrl.Controllers.Models;
    using MicroUrl.Urls;

    [Route("api/microurl")]
    [ApiController]
    public class UrlController : Controller
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] CreateUrlModel urlModel)
        {
            try
            {
                var key = await _urlService.SaveAsync(urlModel.Url, urlModel.Key);
                return new JsonResult(new {Key = key}) {StatusCode = (int) HttpStatusCode.Created};
            }
            catch (KeyGenerationException)
            {
                return new ConflictResult();
            }
        }
    }
}