namespace Bodrocode.Xunit.Logs;

/// <summary>
///     An empty scope without any logic
/// </summary>
internal sealed class XunitScope : IDisposable
{
    public static XunitScope Instance { get; } = new();

    public void Dispose()
    {
    }
}