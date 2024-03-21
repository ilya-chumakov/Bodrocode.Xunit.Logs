using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bodrocode.Xunit.Logs;

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
        return new XUnitLogger(Output, categoryName);
    }

    //todo mock output
    public static ILoggerFactory CreateLoggerFactory(
        ITestOutputHelper output, 
        LogLevel minLogLevel = LogLevel.Debug)
    {
        var services = new ServiceCollection();
        services.AddLogging(cfg =>
        {
            cfg.SetMinimumLevel(minLogLevel);
        });
        var provider = services.BuildServiceProvider();

        var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
        loggerFactory.AddProvider(new XUnitLoggerProvider(output));
        return loggerFactory;
    }
}