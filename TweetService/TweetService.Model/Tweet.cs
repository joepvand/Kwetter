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
        public Tweet(string tweeterId, string body)
        {
            this.Id = new Guid();
            TweeterId = tweeterId;
            Body = body;

            this.Validate<Tweet, TweetValidator>();
        }
        public Guid Id { get; private set; }
        public string TweeterId { get; private set; }
        public string Body { get; private set; }

        public void SetTweeter(string tweeterId)
        {
            this.TweeterId = tweeterId;
        }
    }

}
