namespace Bodrocode.Xunit.Logs.Tests.Examples;

public class BaseOutputTest_Example : BaseOutputTest
{
    public BaseOutputTest_Example(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void OutputWriteLine_Default_WritesToXunitOutput()
    {
        Output.WriteLine("a");
        Output.WriteLine(1);
    }

    [Fact]
    public void WriteLine_Default_WritesToXunitOutput()
    {
        WriteLine("a");
        WriteLine(1);
    }
}