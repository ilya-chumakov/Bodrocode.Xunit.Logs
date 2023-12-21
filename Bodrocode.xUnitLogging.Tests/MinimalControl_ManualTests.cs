using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Bodrocode.xUnitLogging.Tests;

public class MinimalControl_ManualTests : BaseTest
{
    private readonly LogProducer _sut;

    public MinimalControl_ManualTests(ITestOutputHelper output) : base(output)
    {
        var loggerFactory = XUnitLoggerProvider.CreateLoggerFactory(output);

        _sut = new LogProducer(loggerFactory.CreateLogger<LogProducer>());
    }

    [Fact]
    public void CallDotnetLogger_Default_WritesToXUnitOutput()
    {
        _sut.CallDotnetLogger("foo", LogLevel.Information);
    }
}