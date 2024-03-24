namespace Bodrocode.Xunit.Logs;

public class XunitLoggerOptions
{
    public CategoryNameStyle CategoryName = CategoryNameStyle.Short;

    public LogLevel MinimumLogLevel = LogLevel.Debug;

    //todo timestamps on/off
}

public enum CategoryNameStyle
{
    /// <summary>
    /// Name of the type without its namespace
    /// </summary>
    Short = 0,

    /// <summary>
    /// Gets the fully qualified name of the type, including its namespace (.NET logging default)
    /// </summary>
    Full = 1
}