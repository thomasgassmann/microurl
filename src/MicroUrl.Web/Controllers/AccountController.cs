namespace MicroUrl.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using MicroUrl.Web.Authentication;
    using MicroUrl.Web.Controllers.Models;

    [ApiController]
    [Route("api/accounts")]
    public class AccountController : Controller
    {
        private readonly IUserManager _userManager;

        public AccountController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpModel signUp)
        {
            try
            {
                await _userManager.SignUpAsync(signUp.UserName, signUp.Password);
                return Ok();
            }
            catch (UserAlreadyExistsException ex)
            {
                ModelState.AddModelError(nameof(signUp.UserName), ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
