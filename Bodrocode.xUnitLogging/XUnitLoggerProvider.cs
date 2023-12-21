using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Bodrocode.xUnitLogging;

public class XUnitLoggerProvider : ILoggerProvider
{
    public XUnitLoggerProvider(ITestOutputHelper output)
    {
        Output = output;
    }

    public ITestOutputHelper Output { get; }

    public void Dispose()
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new XUnitLogger(categoryName, Output);
    }

    //todo mock output
    public static ILoggerFactory CreateLoggerFactory(ITestOutputHelper output)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        var provider = services.BuildServiceProvider();
        var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
        loggerFactory.AddProvider(new XUnitLoggerProvider(output));
        return loggerFactory;
    }
}