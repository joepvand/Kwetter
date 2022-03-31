using Helpers;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Application;
using Mapster;

namespace ProfileService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileApp profileApp;

        public ProfileController(ProfileApp profileApp)
        {
            this.profileApp = profileApp;
        }

        [HttpGet(Name = "GetProfile")]
        public Profile Get()
        {
            var profile = profileApp.GetProfile(HttpContext.GetUserId());


            return profile.AsDto(false);
        }

        [HttpGet("{profileId}")]
        public Profile GetById(string profileId)
        {
            var profile = profileApp.GetProfile(Guid.Parse(profileId));
            var blocked = profileApp.GetProfile(HttpContext.GetUserId()).BlockedUsers.Any(x => x == Guid.Parse(profileId));
            return profile.AsDto(blocked);
        }
    }
}