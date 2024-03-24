using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Bodrocode.Xunit.Logs.Tests;

public class XunitLogger_IntegrationTests : BaseOutputTest
{
    private readonly LogProducer _logProducer;
    private readonly Mock<ITestOutputHelper> _outputMock;

    public XunitLogger_IntegrationTests(ITestOutputHelper output) : base(output)
    {
        _outputMock = new Mock<ITestOutputHelper>();

        var services = new ServiceCollection();
        services.AddLogging(cfg =>
        {
            cfg.SetMinimumLevel(LogLevel.Information);
        });
        var provider = services.BuildServiceProvider();
        var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
        loggerFactory.AddProvider(new XunitLoggerProvider(_outputMock.Object, cfg =>
        {
            //Trace and Debug events will NOT be logged as the common threshold set to Information above
            cfg.MinimumLogLevel = LogLevel.Trace;
        }));

        _logProducer = new LogProducer(loggerFactory.CreateLogger<LogProducer>());
    }

    [Fact]
    public void CallDotnetLogger_AboveMinLevel_XunitHelperIsCalled()
    {
        string input = "foo";

        //Act
        _logProducer.CallDotnetLogger(input, LogLevel.Warning);

        //Assert
        _outputMock.Verify(m =>
                m.WriteLine(It.Is<string>(x => x.EndsWith(input))),
            Times.Once());
    }

    [Fact]
    public void CallDotnetLogger_BelowMinLevel_XunitHelperIsNotCalled()
    {
        string input = "foo";

        //Act
        _logProducer.CallDotnetLogger(input, LogLevel.Debug);

        //Assert
        _outputMock.Verify(m =>
                m.WriteLine(It.IsAny<string>()),
            Times.Never());
    }
}