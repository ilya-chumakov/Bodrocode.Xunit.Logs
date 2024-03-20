namespace Bodrocode.xUnitLogging;

public static class TestOutputHelperExtensions
{
    public static void WriteLine<T>(this ITestOutputHelper output, T input)
        where T : struct
    {
        output.WriteLine(input.ToString());
    }
}