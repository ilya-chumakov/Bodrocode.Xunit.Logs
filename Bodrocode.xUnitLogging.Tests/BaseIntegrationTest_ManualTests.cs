using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Bodrocode.xUnitLogging.Tests;

public class BaseIntegrationTest_ManualTests : BaseIntegrationTest
{
    private readonly LogProducer _sut;

    public BaseIntegrationTest_ManualTests(ITestOutputHelper console) : base(console)
    {
        _sut = new LogProducer(LoggerFactory.CreateLogger<LogProducer>());
    }

    [Fact]
    public void Do_Default_WritesToXUnitOutput()
    {
        _sut.Do();
    }
}