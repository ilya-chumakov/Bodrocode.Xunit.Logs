using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Bodrocode.Xunit.Logs.Tests.Examples;

public class MinimalControl_Example : BaseTest
{
    private readonly LogProducer _sut;

    public MinimalControl_Example(ITestOutputHelper output) : base(output)
    {
        var loggerFactory = XUnitLoggerProvider.CreateLoggerFactory(output);

        _sut = new LogProducer(loggerFactory.CreateLogger<LogProducer>());
    }

    [Fact]
    public void CallDotnetLogger_Default_WritesToXUnitOutput()
    {
        _sut.CallDotnetLogger("foo", LogLevel.Debug);
    }
}