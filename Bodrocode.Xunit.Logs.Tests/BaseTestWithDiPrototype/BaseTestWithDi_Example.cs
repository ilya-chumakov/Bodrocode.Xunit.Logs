namespace Bodrocode.Xunit.Logs.Tests.BaseTestWithDiPrototype;

public class BaseTestWithDi_Example : BaseTestWithDi
{
    private readonly LogProducer _sut;

    public BaseTestWithDi_Example(ITestOutputHelper console) : base(console)
    {
        _sut = new LogProducer(LoggerFactory.CreateLogger<LogProducer>());
    }

    [Fact]
    public void CallDotnetLogger_Default_WritesToXUnitOutput()
    {
        _sut.CallDotnetLogger("foo");
    }
}