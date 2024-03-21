using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Bodrocode.Xunit.Logs.Tests;

public class XunitLoggerProvider_Tests : BaseOutputTest
{
    private readonly LogProducer _logProducer;
    private readonly Mock<ITestOutputHelper> _testOutputHelperMock;

    public XunitLoggerProvider_Tests(ITestOutputHelper output) : base(output)
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
            new XunitLoggerProvider(_testOutputHelperMock.Object));

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