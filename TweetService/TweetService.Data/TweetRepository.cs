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
            _repo.Tweets.Remove(await _repo.Tweets.FindAsync(guid));

            await _repo.SaveChangesAsync();

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