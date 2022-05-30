using Google.Cloud.Language.V1;
using MassTransit;
using MessagingModels;

namespace PostScanner.Consumers;

public class PostPostedConsumer : IConsumer<IPostReadyForReviewEvent>
{
    public async Task Consume(ConsumeContext<IPostReadyForReviewEvent> context)
    {
        await Console.Out.WriteLineAsync($"Post {context.Message.PostId} is ready for review");
        var analyze = GoogleCloudClient.AnalyzeEntities(context.Message.Body);

        if (analyze.Where(x => x.Sentiment.Score < 0.5 )
            .Any(x => x.Type is Entity.Types.Type.Organization or Entity.Types.Type.Person))
        {
            await Console.Out.WriteLineAsync($"Post {context.Message.PostId} is not suitable for kwetter");
            await context.Publish<IPostRejectedEvent>(new
            {
                Id = context.Message.PostId
            });
        }
        else
        {
            await Console.Out.WriteLineAsync($"Post {context.Message.PostId} is suitable for kwetter");
        }
    }
}