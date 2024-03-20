using Xunit.Abstractions;

namespace Bodrocode.Xunit.Logs.Tests.Examples;

public class BaseTest_Example : BaseTest
{
    public BaseTest_Example(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void OutputWriteLine_Default_WritesToXUnitOutput()
    {
        Output.WriteLine("a");
        Output.WriteLine(1);
    }

    [Fact]
    public void WriteLine_Default_WritesToXUnitOutput()
    {
        WriteLine("a");
        WriteLine(1);
    }
}