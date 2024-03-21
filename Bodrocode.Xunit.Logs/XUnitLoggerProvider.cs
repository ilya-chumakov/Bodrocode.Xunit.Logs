using Microsoft.Extensions.DependencyInjection;

namespace Bodrocode.Xunit.Logs;

public class XunitLoggerProvider : ILoggerProvider
{
    public XunitLoggerProvider(ITestOutputHelper output)
    {
        Output = output;
    }

    public ITestOutputHelper Output { get; }

    public void Dispose()
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new XunitLogger(Output, categoryName);
    }

    [Obsolete]
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
        loggerFactory.AddProvider(new XunitLoggerProvider(output));
        return loggerFactory;
    }
}