namespace MicroUrl.Web.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using MicroUrl.Storage;
    using MicroUrl.Web.Controllers.Extensions;
    using MicroUrl.Web.Controllers.Models;
    using MicroUrl.Web.Urls;

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
                var key = await _urlService.SaveAsync(urlModel.Url, urlModel.Key?.ToLower());
                return this.CreatedUrl(key);
            }
            catch (KeyGenerationException)
            {
                return new ConflictResult();
            }
        }
    }
}