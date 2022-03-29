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
        public IActionResult Get()
        {
            var result = this.tweetApp.GetTweetByUser(HttpContext.GetUserId());
            return Ok(result);
        }

        [HttpGet("{tweetId}", Name = nameof(GetById))]
        public IActionResult GetById([FromRoute] string tweetId)
        {
            var result = this.tweetApp.GetTweetById(Guid.Parse(tweetId));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostTweetRequest postRequest)
        {
            var userid = HttpContext.GetUserId();
            var domainModel = postRequest.AsDomainModel(HttpContext.GetUserId());
            var result = await tweetApp.AddTweet(domainModel);
            return CreatedAtRoute(nameof(GetById), new { tweetId = result.Id }, result);
        }

        [HttpDelete("/{tweetId}")]
        public async Task<IActionResult> Delete([FromRoute]string tweetId)
        {
            await tweetApp.DeleteTweet(Guid.Parse(tweetId));
            return NoContent();
        }
    }
}
