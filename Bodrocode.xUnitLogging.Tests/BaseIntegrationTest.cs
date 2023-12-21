using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Bodrocode.xUnitLogging;

public class BaseIntegrationTest : BaseTest
{
    private readonly IServiceProvider _provider;
    private readonly ServiceCollection _services;

    public BaseIntegrationTest(ITestOutputHelper console) : base(console)
    {
        Configuration = BuildConfiguration();

        _services = CreateServiceCollectionWithLogging();

        ConfigureServices(_services);

        _provider = _services.BuildServiceProvider();

        LoggerFactory = CreateLoggerFactory();

        Configure(Provider);
    }

    private IConfigurationRoot BuildConfiguration()
    {
        var set = new Action<string, string>(Environment.SetEnvironmentVariable);

        //set("DISABLE_HTTP_CLIENT_INPUT_VALIDATION", true.ToString());

        var root = new ConfigurationBuilder()
            //.AddEnvironmentVariables()
            .Build();

        return root;
    }

    private static ServiceCollection CreateServiceCollectionWithLogging()
    {
        var services = new ServiceCollection();
        services.AddLogging(b =>
        {
            //b.AddConsole(x =>
            //{
            //    x.IncludeScopes = false;
            //});
            b.SetMinimumLevel(LogLevel.Debug);
        });
        return services;
    }

    /// <summary>
    ///     Логгер, созданный этой фабрикой - будет писать в xUnit Output (отображается в R#).
    /// </summary>
    protected ILoggerFactory LoggerFactory { get; }

    public IConfiguration Configuration { get; set; }

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
    private ILoggerFactory CreateLoggerFactory()
    {
        var loggerFactory = Provider.GetRequiredService<ILoggerFactory>();

        //if (_options.CreateXUnitLoggerFactory)
        {
            loggerFactory.AddProvider(new XUnitLoggerProvider(this));
        }

        return loggerFactory;
    }

    protected virtual void ConfigureServices(IServiceCollection services) { }

    protected virtual void Configure(IServiceProvider provider) { }

    //protected virtual void ConfigureOptions(IntegrationTestOptions options) { }
}