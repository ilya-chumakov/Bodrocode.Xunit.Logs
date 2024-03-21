namespace Bodrocode.Xunit.Logs.Tests.Examples;

public class LoggerOnly_Example
{
    private readonly LogProducer _producer0;
    private readonly LogProducer _producer1;
    private readonly LogProducer _producer2;

    public LoggerOnly_Example(ITestOutputHelper output)
    {
        _producer0 = new LogProducer(new XunitLogger<LogProducer>(output));
        _producer1 = new LogProducer(new XunitLogger<LogProducer>(output, nameof(LogProducer)));
        _producer2 = new LogProducer(output.For<LogProducer>());
    }

    [Fact]
    public void CallDotnetLogger_Default_WritesToXunitOutput()
    {
        _producer0.CallDotnetLogger(nameof(_producer1), LogLevel.Trace);
        _producer1.CallDotnetLogger(nameof(_producer1), LogLevel.Trace);
        _producer2.CallDotnetLogger(nameof(_producer2), LogLevel.Trace);
    }
}