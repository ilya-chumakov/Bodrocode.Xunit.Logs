namespace Bodrocode.Xunit.Logs.Tests.Examples;

public class LoggerOnly_Example
{
    private readonly LogProducer _producer1;
    private readonly LogProducer _producer2;

    public LoggerOnly_Example(ITestOutputHelper output)
    {
        _producer1 = new LogProducer(new XunitLogger<LogProducer>(output));
        _producer2 = new LogProducer(output.For<LogProducer>());
    }

    [Fact]
    public void CallDotnetLogger_Default_WritesToXunitOutput()
    {
        _producer1.CallDotnetLogger("producer1 message", LogLevel.Information);
        _producer2.CallDotnetLogger("producer2 message", LogLevel.Information);
    }
}