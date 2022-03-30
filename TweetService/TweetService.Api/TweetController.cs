using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TweetService.Api.Models;
using TweetService.Api.Models.Requests;
using TweetService.Application;

namespace TweetService.Api
{
    [ApiController]
    [Route("[controller]/Tweet")]
    public class TweetController : ControllerBase
    {
        private readonly TweetApplication tweetApp;

        public TweetController(TweetApplication tweetApp)
        {
            this.tweetApp = tweetApp;
        }

        [HttpGet("/Feed")]
        public IActionResult GetFeed()
        {
            var result = this.tweetApp.GetFeedByUser(HttpContext.GetUserId());
            return Ok(result);
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

            if (result.TweeterId == HttpContext.GetUserId()
                || HttpContext.GetUserRole() == Role.Admin)
            {
                return Ok(result);
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostTweetRequest postRequest)
        {
            var result = await tweetApp.AddTweet(postRequest.AsDomainModel(HttpContext.GetUserId()));
            return CreatedAtRoute(nameof(GetById), new { tweetId = result.Id }, result);
        }

        [HttpDelete("/{tweetId}")]
        public async Task<IActionResult> Delete([FromRoute] string tweetId)
        {
            var result = this.tweetApp.GetTweetById(Guid.Parse(tweetId));

            if (result.TweeterId == HttpContext.GetUserId()
                || HttpContext.GetUserRole() == Role.Admin)
            {
                await tweetApp.DeleteTweet(Guid.Parse(tweetId));
                return NoContent();
            }

            return Unauthorized();
        }
    }
}
