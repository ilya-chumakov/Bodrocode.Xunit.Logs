using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Bodrocode.xUnitLogging.Tests;

public class BaseIntegrationTest : BaseTest
{
    private readonly IServiceProvider _provider;

    public BaseIntegrationTest(ITestOutputHelper console) : base(console)
    {
        var services = CreateServiceCollectionWithLogging();

        _provider = services.BuildServiceProvider();

        LoggerFactory = CreateLoggerFactory();
    }

    /// <summary>
    ///     Loggers from this factory will write to xUnit Output (shown in R# test runner output).
    /// </summary>
    protected ILoggerFactory LoggerFactory { get; }

    protected IServiceProvider Provider
    {
        get
        {
            if (_provider == null)
            {
                throw new NotSupportedException(
                    "Do not access a provider from a constructor or ConfigureServices call. Access the provider only from test methods");
            }
            return _provider;
        }
    }

    private static ServiceCollection CreateServiceCollectionWithLogging()
    {
        var services = new ServiceCollection();
        services.AddLogging(b => { b.SetMinimumLevel(LogLevel.Debug); });
        return services;
    }

    private ILoggerFactory CreateLoggerFactory()
    {
        var loggerFactory = Provider.GetRequiredService<ILoggerFactory>();
        loggerFactory.AddProvider(new XUnitLoggerProvider(Output));
        return loggerFactory;
    }
}