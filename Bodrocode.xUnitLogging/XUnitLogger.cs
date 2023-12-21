using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Bodrocode.xUnitLogging;

public class XUnitLogger : ILogger
{
    public XUnitLogger(string categoryName, ITestOutputHelper writer)
    {
        CategoryName = categoryName;
        ShortCategoryName = GetShortCategoryName(CategoryName);
        Writer = writer;
    }

    protected ITestOutputHelper Writer { get; }

    protected string ShortCategoryName { get; }

    protected string CategoryName { get; }

    public void Log<TState>(LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception exception,
        Func<TState, Exception, string> formatter)
    {
        //todo why is this here?
        if (!IsEnabled(logLevel))
            return;

        if (formatter == null)
            throw new ArgumentNullException(nameof(formatter));

        string message = formatter(state, exception);
        if (string.IsNullOrEmpty(message) && exception == null)
            return;

        string logLevelString = GetLogLevelString(logLevel);

        string line = $"{logLevelString}: {ShortCategoryName}: {message}";

        Writer.WriteLine(line);

        if (exception != null)
            Writer.WriteLine(exception.ToString());
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return new XUnitScope();
    }

    private string GetShortCategoryName(string categoryName)
    {
        int lastDotIndex = categoryName.LastIndexOf('.');

        return lastDotIndex > 0 ? categoryName.Substring(lastDotIndex) : categoryName;
    }

    private static string GetLogLevelString(LogLevel logLevel)
    {
        switch (logLevel)
        {
            case LogLevel.Trace:
                return "trce";
            case LogLevel.Debug:
                return "dbug";
            case LogLevel.Information:
                return "info";
            case LogLevel.Warning:
                return "warn";
            case LogLevel.Error:
                return "fail";
            case LogLevel.Critical:
                return "crit";
            default:
                throw new ArgumentOutOfRangeException(nameof(logLevel));
        }
    }
}