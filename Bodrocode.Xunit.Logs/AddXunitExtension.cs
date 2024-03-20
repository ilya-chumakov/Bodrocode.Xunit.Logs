using Microsoft.Extensions.Logging;

namespace Bodrocode.Xunit.Logs.Tests;

public static class AddXunitExtension
{
    public static void AddXunit(this ILoggingBuilder loggingBuilder, ITestOutputHelper output)
    {
        loggingBuilder.AddProvider(new XUnitLoggerProvider(output));
    }
}