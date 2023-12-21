using Microsoft.Extensions.Logging;

namespace Bodrocode.xUnitLogging.Tests;

public class LogProducer
{
    private readonly ILogger<LogProducer> _logger;

    public LogProducer(ILogger<LogProducer> logger)
    {
        _logger = logger;
    }

    public void Do()
    {
        _logger.LogInformation("Do something");
    }
}