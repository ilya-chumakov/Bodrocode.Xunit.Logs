namespace Bodrocode.Xunit.Logs;

public class XunitLogger<T> : XunitLogger, ILogger<T>
{
    public XunitLogger(ITestOutputHelper output) : base(output, typeof(T).Name)
    {
    }
}

public class XunitLogger : ILogger
{
    public XunitLogger(ITestOutputHelper output, string categoryName)
    {
        CategoryName = categoryName;
        ShortCategoryName = GetShortCategoryName(CategoryName);
        Output = output;
    }

    protected ITestOutputHelper Output { get; }

    protected string ShortCategoryName { get; }

    protected string CategoryName { get; }

    public void Log<TState>(LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;
        
        if (formatter == null)
            throw new ArgumentNullException(nameof(formatter));

        string message = formatter(state, exception);

        if (string.IsNullOrEmpty(message) && exception == null)
            return;

        string logLevelString = GetLogLevelString(logLevel);

        //optimize contatenation?
        string line = $"{logLevelString}: {ShortCategoryName}: {message}";

        Output.WriteLine(line);

        if (exception != null)
            Output.WriteLine(exception.ToString());
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None;
    }

    public IDisposable BeginScope<TState>(TState state)
        where TState : notnull
    {
        return XunitScope.Instance;
    }

    private string GetShortCategoryName(string categoryName)
    {
        int lastDotIndex = categoryName.LastIndexOf('.');

        return lastDotIndex > 0 ? categoryName.Substring(lastDotIndex + 1) : categoryName;
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