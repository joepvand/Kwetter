using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AuthService.Model;

namespace AuthService.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        public RoleController(UserManager<ApplicationUser> usermanager, RoleManager<IdentityRole> roleManager)
        {
            Usermanager = usermanager;
            RoleManagerManager = roleManager;
        }

        public UserManager<ApplicationUser> Usermanager { get; }
        public RoleManager<IdentityRole> RoleManagerManager { get; }

        [HttpPost]
        public async Task<IActionResult> AddRole(string Rolename)
        {
            var res = await RoleManagerManager.CreateAsync(new IdentityRole(Rolename));
            if (res.Succeeded)
                return Ok();

            string errors = "";
            foreach (var error in res.Errors)
            {
                errors += error.Description;
            }

            return BadRequest(errors);
        }

    }
}
