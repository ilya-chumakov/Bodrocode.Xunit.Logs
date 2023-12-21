using Xunit.Abstractions;

namespace Bodrocode.xUnitLogging.Tests;

public class BaseTest_ManualTests : BaseTest
{
    public BaseTest_ManualTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void WriteLine_Default_WritesToXUnitOutput()
    {
        Output.WriteLine("a");
        Output.WriteLine(1);
    }
}