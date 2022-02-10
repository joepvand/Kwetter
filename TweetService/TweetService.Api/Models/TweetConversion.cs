using TweetService.Api.Models.Requests;
using TweetService.DomainModel;

namespace TweetService.Api.Models
{
    public static class TweetConversion
    {
        public static Tweet AsDomainModel(this PostTweetRequest request, Guid tweeter)
        {
            return new Tweet(tweeter, request.Body);
        }
    }
}
