using Google.Cloud.Language.V1;

namespace PostScanner;

public class GoogleCloudClient
{
    public static List<Entity> AnalyzeEntities(string content)
    {
        var client = LanguageServiceClient.Create();
        var document = Document.FromPlainText(content);
        var response = client.AnalyzeEntitySentiment(document);

        return response.Entities.ToList();
    }
}