using Xunit.Abstractions;

namespace Bodrocode.xUnitLogging;

public class BaseTest : IXUnitLogWriter
{
    protected readonly ITestOutputHelper Output;

    public BaseTest(ITestOutputHelper output)
    {
        Output = output;
    }

    public void WriteLine(string str)
    {
        Output.WriteLine(str);
    }
}