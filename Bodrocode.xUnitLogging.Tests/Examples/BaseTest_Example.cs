using Xunit.Abstractions;

namespace Bodrocode.xUnitLogging.Tests.Examples;

public class BaseTest_Example : BaseTest
{
    public BaseTest_Example(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void WriteLine_Default_WritesToXUnitOutput()
    {
        Output.WriteLine("a");
        Output.WriteLine(1);
    }
}