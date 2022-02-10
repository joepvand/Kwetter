

using TweetService.Data;
using TweetService.DomainModel;

namespace TweetService.Application
{
    public class TweetApplication
    {
        private readonly ITweetRepository tweetRepository;

        public TweetApplication(ITweetRepository tweetRepository)
        {
            this.tweetRepository = tweetRepository;
        }
        public Task AddTweet(Tweet tweet)
        {
            return this.tweetRepository.AddTweetAsync(new Data.Models.Tweet()
            { 
                Body = tweet.Body,
                TweeterId = tweet.TweeterId,
            });
        }

        public Task DeleteTweet(Guid guid)
        {
            return this.tweetRepository.DeleteTweetAsync(guid);
        }

        public List<Tweet> GetTweetByUser(Guid userId)
        {
            return this.tweetRepository.GetTweetsByUser(userId)
                .Select(x=> new Tweet(x.TweeterId, x.Body)).ToList();
        }

        public List<Tweet> GetTweets()
        {
            return this.tweetRepository.GetTweets()
                .Select(x => new Tweet(x.TweeterId, x.Body)).ToList(); ;
        }
    }
}