using TweetService.Api.Models.Requests;
using TweetService.DomainModel;

namespace TweetService.Api.Models
{
    public static class TweetConversion
    {
        public static DomainModel.Tweet AsDomainModel(this PostTweetRequest request, Guid tweeter)
        {
            return new DomainModel.Tweet(tweeter, request.Body);
        }

        public static Api.Models.Tweet AsDTO(this DomainModel.Tweet tweet)
        {
            return new Api.Models.Tweet()
            {
                Body = tweet.Body,
            };
        }
    }
}
