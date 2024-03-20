using Microsoft.Extensions.Logging;

namespace Bodrocode.Xunit.Logs.Tests;

public class LogProducer
{
    private readonly ILogger<LogProducer> _logger;

    public LogProducer(ILogger<LogProducer> logger)
    {
        _logger = logger;
    }

    public void CallDotnetLogger(string text, LogLevel logLevel = LogLevel.Information)
    {
        _logger.Log(logLevel, text);
    }
}