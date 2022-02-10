using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using AuthService.Api.Constants;
using AuthService.Model;
using AuthService.Model.Requests;

namespace AuthService.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> role)
        {
            UserManager = userManager;
            RoleManager = role;
        }

        public UserManager<ApplicationUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }

        [HttpPost("")]
        public async Task<IActionResult> Register([FromBody] SignupRequest request)
        {
            var user = new ApplicationUser(request);
            var res = await UserManager.CreateAsync(user, request.Password);
            await UserManager.AddToRoleAsync(user, Roles.User);

            if (res.Succeeded)
                return Ok();

            string errors = "";
            foreach (var error in res.Errors)
            {
                errors += error.Description + "\n";
            }

            return BadRequest(errors);
        }
    }
}
