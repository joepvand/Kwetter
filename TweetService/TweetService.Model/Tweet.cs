using FluentValidation;
using Helpers;
using TweetService.DomainModel.Validators;

namespace TweetService.DomainModel
{
    public class Tweet
    {
        public Tweet(Guid tweeterId, string body)
        {
            TweeterId = tweeterId;
            Body = body;

            this.Validate<Tweet, TweetValidator>();
        }
        public Guid TweeterId { get; }
        public string Body { get; }
    }

}
