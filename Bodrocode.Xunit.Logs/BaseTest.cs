namespace Bodrocode.Xunit.Logs;

public class BaseTest
{
    protected readonly ITestOutputHelper Output;

    public BaseTest(ITestOutputHelper output)
    {
        Output = output;
    }

    public void WriteLine(string str) 
        => Output.WriteLine(str);

    public void WriteLine(string format, params object[] args) 
        => Output.WriteLine(format, args);

    public void WriteLine<T>(T input)
        where T : struct =>
        Output.WriteLine(input.ToString());
}