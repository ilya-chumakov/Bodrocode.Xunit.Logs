using Microsoft.Extensions.Logging;

namespace Bodrocode.xUnitLogging;

public class XUnitLoggerProvider : ILoggerProvider
{
    public IXUnitLogWriter Writer { get; private set; }

    public XUnitLoggerProvider(IXUnitLogWriter writer)
    {
        Writer = writer;
    }

    public void Dispose() { }

    public ILogger CreateLogger(string categoryName)
    {
        return new XUnitLogger(categoryName, Writer);
    }
}