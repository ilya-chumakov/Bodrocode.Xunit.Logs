using Microsoft.Extensions.Logging;

namespace Bodrocode.Xunit.Logs;

public static class ForExtension
{
    public static ILogger<TProducer> For<TProducer>(this ITestOutputHelper output)
    {
        return new XUnitLogger<TProducer>(output, typeof(TProducer).Name);
    }
}