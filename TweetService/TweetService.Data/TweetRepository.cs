using MassTransit;
using MessagingModels;
using TweetService.Data.Context;
using TweetService.Data.Models;

namespace TweetService.Data
{
    public class TweetRepository : ITweetRepository
    {
        private readonly TweetContext _repo;
        private readonly IPublishEndpoint publishEndpoint;

        public TweetRepository(TweetContext repo, MassTransit.IPublishEndpoint publishEndpoint)
        {
            this._repo = repo;
            this.publishEndpoint = publishEndpoint;
        }
        public async Task AddTweetAsync(Tweet tweet)
        {
            await _repo.AddAsync(tweet);
            await _repo.SaveChangesAsync();
            await publishEndpoint.Publish<ITweetTweetedEvent>(new
            {
                Id = tweet.Id,
                TweeterId = tweet.TweeterId,
                Body = tweet.Body
            });
        }

        public async Task DeleteTweetAsync(Guid guid)
        {
            var tweet = await _repo.Tweets.FindAsync(guid);
            if (tweet != null)
            {
                _repo.Tweets.Remove(tweet);

                await _repo.SaveChangesAsync();
            }

            throw new Exception($"Tweet with id '{guid}' not found!");

        }

        public IQueryable<Tweet> GetTweetsByUser(string userId)
        {
            return _repo.Tweets.Where(tweet => tweet.TweeterId == userId);
        }

        public IQueryable<Tweet> GetTweets()
        {
            return _repo.Tweets;
        }
    }
}