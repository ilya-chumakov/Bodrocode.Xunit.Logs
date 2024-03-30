[![NuGet](http://img.shields.io/nuget/v/Bodrocode.Xunit.Logs.svg)](https://www.nuget.org/packages/Bodrocode.Xunit.Logs/)
[![Publish Nuget Package](https://github.com/ilya-chumakov/Bodrocode.Xunit.Logs/actions/workflows/publish.yml/badge.svg?branch=master&event=pull_request)](https://github.com/ilya-chumakov/Bodrocode.Xunit.Logs/actions/workflows/publish.yml)

# Bodrocode.Xunit.Logs

Integration between .NET logging and xUnit [output](https://xunit.net/docs/capturing-output).
In short, it adds logs to the output window of your test runner.

### Creating a standalone logger:
```cs
public ManualCreation_Example(ITestOutputHelper output)
{
    _fooService = new FooService(output.For<FooService>());
    
    //or (the same)
    _fooService = new FooService(new XunitLogger<FooService>(output));
}
```

### Integration with Microsoft.Extensions.DependencyInjection:
```cs
public DependencyInjection_Example(ITestOutputHelper output)
{
    var services = new ServiceCollection();
    services.AddLogging(cfg =>
    {
        cfg.AddXunit(output);
    });
    services.AddTransient<FooService>();
    var provider = services.BuildServiceProvider();

    _fooService = provider.GetRequiredService<FooService>();
}
```

### Customization:

```cs
var logger = new XunitLogger(output, "fooCategory", cfg =>
{
    cfg.MinimumLogLevel = LogLevel.Warning;
    cfg.CategoryName = CategoryNameStyle.Short;
});
```

### Testability:

```cs
var mock = new Mock<ITestOutputHelper>();

var logger = mock.Object.For<FooService>();

logger.Log(LogLevel.Warning, "fooMessage");

mock.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Once());
```
