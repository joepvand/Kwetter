using TweetService.Data.Context;
using TweetService.Data.Models;

namespace TweetService.Data
{
    public class TweetRepository : ITweetRepository
    {
        private readonly TweetContext _repo;

        public TweetRepository(TweetContext repo)
        {
            this._repo = repo;
        }
        public async Task AddTweetAsync(Tweet tweet)
        {
            await _repo.AddAsync(tweet);
            await _repo.SaveChangesAsync();
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

        public IReadOnlyCollection<Tweet> GetTweetsByUser(Guid userId)
        {
            return _repo.Tweets.Where(tweet => tweet.TweeterId == userId).ToList();
        }

        public IReadOnlyCollection<Tweet> GetTweets()
        {
            return _repo.Tweets.ToList();
        }
    }
}