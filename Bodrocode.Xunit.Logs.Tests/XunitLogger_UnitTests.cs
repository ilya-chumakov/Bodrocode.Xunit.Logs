using Moq;

namespace Bodrocode.Xunit.Logs.Tests;

public class XunitLogger_UnitTests
{
    private readonly Mock<ITestOutputHelper> _outputMock = new();
    private readonly ITestOutputHelper _output;

    public XunitLogger_UnitTests()
    {
        _output = _outputMock.Object;
    }

    [Theory]
    [InlineData(LogLevel.Information, false)]
    [InlineData(LogLevel.Warning, true)]
    [InlineData(LogLevel.Error, true)]
    public void IsEnabled_Theory_OK(LogLevel input, bool expected)
    {
        var logger = new XunitLogger(_output, "foo", cfg => { cfg.MinimumLogLevel = LogLevel.Warning; });

        //Act
        bool actual = logger.IsEnabled(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CategoryNameStyle_Short_WritesShort()
    {
        var logger = new XunitLogger(_output, "foo.bar", cfg => { cfg.CategoryName = CategoryNameStyle.Short; });

        //Act
        logger.Log(LogLevel.Warning, nameof(CategoryNameStyle_Short_WritesShort));

        _outputMock.Verify(m => m.WriteLine(
                It.Is<string>(x => !x.Contains("foo") && x.Contains("bar"))
            ),
            Times.Once());
    }
}