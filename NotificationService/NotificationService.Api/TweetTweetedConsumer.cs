using MassTransit;
using MessagingModels;

namespace NotificationService.Api
{
    public class TweetTweetedConsumer : IConsumer<ITweetTweetedEvent>
    {

        public async Task Consume(ConsumeContext<ITweetTweetedEvent> context)
        {
            Console.WriteLine("Recieved tweet: " + context.Message.Body);

        }

      
    }
}
