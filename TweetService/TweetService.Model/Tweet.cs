using FluentValidation;
using Helpers;
using Mapster;
using TweetService.DomainModel.Validators;

namespace TweetService.DomainModel
{
    public class Tweet
    {
        public Tweet()
        {

        }
        public Tweet(Guid tweeterId, string body)
        {
            this.Id = new Guid();
            TweeterId = tweeterId;
            Body = body;
            this.TweetedAt = DateTime.Now;

            this.Validate<Tweet, TweetValidator>();
        }
        public Guid Id { get; private set; }
        public Guid TweeterId { get; private set; }
        public string Body { get; private set; }
        public DateTime TweetedAt { get; private set; }

        public void SetTweeter(Guid tweeterId)
        {
            this.TweeterId = tweeterId;
        }
    }

}
