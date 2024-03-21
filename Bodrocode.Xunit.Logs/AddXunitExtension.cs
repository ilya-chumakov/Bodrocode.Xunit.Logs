namespace Bodrocode.Xunit.Logs;

public static class AddXunitExtension
{
    public static ILoggingBuilder AddXunit(this ILoggingBuilder loggingBuilder, ITestOutputHelper output)
    {
        return loggingBuilder.AddProvider(new XunitLoggerProvider(output));
    }

    public static void AddXunitProvider(this ILoggerFactory factory, ITestOutputHelper output)
    {
        factory.AddProvider(new XunitLoggerProvider(output));
    }
}