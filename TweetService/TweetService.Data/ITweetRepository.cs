using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetService.Data.Models;

namespace TweetService.Data
{
    public interface ITweetRepository
    {
        public Task AddTweetAsync(Tweet tweet);
        public Task DeleteTweetAsync(Guid guid);
        public IReadOnlyCollection<Tweet> GetTweets();
        public IReadOnlyCollection<Tweet> GetTweetsByUser(string userId);
    }
}
