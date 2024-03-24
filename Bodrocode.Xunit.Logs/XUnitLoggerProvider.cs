using Microsoft.Extensions.DependencyInjection;

namespace Bodrocode.Xunit.Logs;

public class XunitLoggerProvider : ILoggerProvider
{
    public XunitLoggerProvider(ITestOutputHelper output, Action<XunitLoggerOptions>? configure = null)
    {
        _output = output;
        _configure = configure;
    }

    private readonly ITestOutputHelper _output;
    private readonly Action<XunitLoggerOptions>? _configure;

    public void Dispose()
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new XunitLogger(_output, categoryName, _configure);
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