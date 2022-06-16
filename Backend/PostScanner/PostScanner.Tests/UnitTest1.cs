using Xunit;

namespace PostScanner.Tests;

public class UnitTest1
{
    [Fact]
    public void MeanIsFalse()
    {
        Assert.False(GoogleCloudClient.IsApproved("I hate kwetter so much! its a horrible platform."));
    }
}