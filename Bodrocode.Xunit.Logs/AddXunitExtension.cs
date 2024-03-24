namespace Bodrocode.Xunit.Logs;

public static class AddXunitExtension
{
    public static ILoggingBuilder AddXunit(
        this ILoggingBuilder loggingBuilder, 
        ITestOutputHelper output, 
        Action<XunitLoggerOptions>? configure = null)
    {
        return loggingBuilder.AddProvider(new XunitLoggerProvider(output, configure));
    }

    public static void AddXunitProvider(
        this ILoggerFactory factory, 
        ITestOutputHelper output, 
        Action<XunitLoggerOptions>? configure = null)
    {
        factory.AddProvider(new XunitLoggerProvider(output, configure));
    }
}