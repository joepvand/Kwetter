using FluentValidation;
using Helpers;
using TweetService.DomainModel.Validators;

namespace TweetService.DomainModel
{
    public class Tweet
    {
        public Tweet(string tweeterId, string body)
        {
            TweeterId = tweeterId;
            Body = body;

            this.Validate<Tweet, TweetValidator>();
        }
        public string TweeterId { get; }
        public string Body { get; }
    }

}
