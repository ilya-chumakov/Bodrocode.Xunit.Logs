using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Bodrocode.xUnitLogging.Tests;

public class FullControl_ManualTests : BaseTest
{
    private readonly LogProducer _sut;

    public FullControl_ManualTests(ITestOutputHelper output) : base(output)
    {
        var services = new ServiceCollection();
        services.AddLogging(cfg =>
        {
            cfg.SetMinimumLevel(LogLevel.Trace);
        });
        var provider = services.BuildServiceProvider();
        var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
        loggerFactory.AddProvider(new XUnitLoggerProvider(output));

        _sut = new LogProducer(loggerFactory.CreateLogger<LogProducer>());
    }

    [Fact]
    public void CallDotnetLogger_Default_WritesToXUnitOutput()
    {
        _sut.CallDotnetLogger("foo", LogLevel.Trace);
    }
}