using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Bodrocode.xUnitLogging;

public class XUnitLoggerProvider : ILoggerProvider
{
    public ITestOutputHelper Writer { get; private set; }

    public XUnitLoggerProvider(ITestOutputHelper writer)
    {
        Writer = writer;
    }

    public void Dispose() { }

    public ILogger CreateLogger(string categoryName)
    {
        return new XUnitLogger(categoryName, Writer);
    }
    
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