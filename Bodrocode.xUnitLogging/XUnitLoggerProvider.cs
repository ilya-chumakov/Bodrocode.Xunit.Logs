using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bodrocode.xUnitLogging;

public class XUnitLoggerProvider : ILoggerProvider
{
    public IXUnitLogWriter Writer { get; private set; }

    public XUnitLoggerProvider(IXUnitLogWriter writer)
    {
        Writer = writer;
    }

    public void Dispose() { }

    //todo unused?
    public ILogger CreateLogger(string categoryName)
    {
        return new XUnitLogger(categoryName, Writer);
    }
    
    public static ILoggerFactory CreateLoggerFactory(IXUnitLogWriter writer)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        var provider = services.BuildServiceProvider();
        var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
        loggerFactory.AddProvider(new XUnitLoggerProvider(writer));
        return loggerFactory;
    }
}