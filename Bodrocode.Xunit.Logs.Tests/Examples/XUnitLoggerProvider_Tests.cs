using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Abstractions;

namespace Bodrocode.Xunit.Logs.Tests.Examples;

public class XUnitLoggerProvider_Tests : BaseTest
{
    private readonly LogProducer _logProducer;
    private readonly Mock<ITestOutputHelper> _testOutputHelperMock;

    public XUnitLoggerProvider_Tests(ITestOutputHelper output) : base(output)
    {
        _testOutputHelperMock = new Mock<ITestOutputHelper>();

        var services = new ServiceCollection();
        services.AddLogging(cfg =>
        {
            //filter
            cfg.SetMinimumLevel(LogLevel.Information);
        });
        var provider = services.BuildServiceProvider();
        var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
        loggerFactory.AddProvider(
            //substitute
            new XUnitLoggerProvider(_testOutputHelperMock.Object));

        _logProducer = new LogProducer(loggerFactory.CreateLogger<LogProducer>());
    }

    [Fact]
    public void CallDotnetLogger_AboveMinLevel_XUnitHelperIsCalled()
    {
        string input = "foo";

        //Act
        _logProducer.CallDotnetLogger(input, LogLevel.Warning);

        //Assert
        _testOutputHelperMock.Verify(m =>
                m.WriteLine(It.Is<string>(x => x.EndsWith(input))),
            Times.Once);
    }

    [Fact]
    public void CallDotnetLogger_BelowMinLevel_XUnitHelperIsNotCalled()
    {
        string input = "foo";

        //Act
        _logProducer.CallDotnetLogger(input, LogLevel.Debug);

        //Assert
        _testOutputHelperMock.Verify(m =>
                m.WriteLine(It.IsAny<string>()),
            Times.Never);
    }
}