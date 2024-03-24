namespace Bodrocode.Xunit.Logs;

public class XunitLogger<T> : XunitLogger, ILogger<T>
{
    public XunitLogger(ITestOutputHelper output, Action<XunitLoggerOptions>? configure = null) : base(output,
        typeof(T).Name, configure)
    {
    }
}

public class XunitLogger : ILogger
{
    private readonly XunitLoggerOptions _options = new();

    public XunitLogger(ITestOutputHelper output, string categoryName, Action<XunitLoggerOptions>? configure = null)
    {
        Output = output;

        configure?.Invoke(_options);

        CategoryName = _options.CategoryName == CategoryNameStyle.Full
            ? categoryName
            : GetShortCategoryName(categoryName);
    }

    protected ITestOutputHelper Output { get; }

    protected string CategoryName { get; }

    public void Log<TState>(LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        ArgumentNullException.ThrowIfNull(formatter);

        string message = formatter(state, exception);

        if (string.IsNullOrEmpty(message) && exception == null)
            return;

        string logLevelString = GetLogLevelString(logLevel);

        //todo optimize contatenation?
        string line = $"{logLevelString}: {CategoryName}: {message}";

        Output.WriteLine(line);

        if (exception != null)
            Output.WriteLine(exception.ToString());
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return (int)logLevel >= (int)_options.MinimumLogLevel;
    }

    public IDisposable BeginScope<TState>(TState state)
        where TState : notnull
    {
        return XunitScope.Instance;
    }

    private string GetShortCategoryName(string categoryName)
    {
        try
        {
            int lastDotIndex = categoryName.LastIndexOf('.');

            return lastDotIndex > 0 ? categoryName.Substring(lastDotIndex + 1) : categoryName;
        }
        catch (Exception ex)
        {
            Output.WriteLine("Error of shortening category name: " + ex);

            return categoryName;
        }
    }

    private static string GetLogLevelString(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => "trce",
            LogLevel.Debug => "dbug",
            LogLevel.Information => "info",
            LogLevel.Warning => "warn",
            LogLevel.Error => "fail",
            LogLevel.Critical => "crit",
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel))
        };
    }
}