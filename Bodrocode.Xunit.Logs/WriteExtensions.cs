namespace Bodrocode.Xunit.Logs;

public static class WriteExtensions
{
    public static void WriteLine<T>(this ITestOutputHelper output, T input)
        where T : struct
    {
        output.WriteLine(input.ToString());
    }
}