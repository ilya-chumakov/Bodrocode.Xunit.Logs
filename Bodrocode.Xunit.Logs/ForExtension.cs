namespace Bodrocode.Xunit.Logs;

public static class ForExtension
{
    public static ILogger<TProducer> For<TProducer>(this ITestOutputHelper output)
    {
        return new XunitLogger<TProducer>(output);
    }
}