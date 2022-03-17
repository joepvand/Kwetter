using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TweetService.Api.Models;
using TweetService.Api.Models.Requests;
using TweetService.Application;

namespace TweetService.Api
{
    [ApiController]
    [Route("[controller]")]
    public class TweetController : ControllerBase
    {
        private readonly TweetApplication tweetApp;

        public TweetController(TweetApplication tweetApp)
        {
            this.tweetApp = tweetApp;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = this.tweetApp.GetTweetByUser(HttpContext.GetUserId());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostTweetRequest postRequest)
        {
            await tweetApp.AddTweet(postRequest.AsDomainModel(Guid.NewGuid()));
            return Ok();
        } 
    }
}
