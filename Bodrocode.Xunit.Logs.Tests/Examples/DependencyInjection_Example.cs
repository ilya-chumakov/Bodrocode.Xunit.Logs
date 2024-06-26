using Microsoft.Extensions.DependencyInjection;

namespace Bodrocode.Xunit.Logs.Tests.Examples;

public class DependencyInjection_Example
{
    private readonly LogProducer _sut;

    public DependencyInjection_Example(ITestOutputHelper output)
    {
        var services = new ServiceCollection();
        services.AddLogging(cfg =>
        {
            cfg.AddXunit(output);
        });
        services.AddTransient<LogProducer>();
        var provider = services.BuildServiceProvider();

        _sut = provider.GetRequiredService<LogProducer>();
    }

    [Fact]
    public void CallDotnetLogger_Default_WritesToXunitOutput()
    {
        _sut.CallDotnetLogger("foo", LogLevel.Warning);
    }
}