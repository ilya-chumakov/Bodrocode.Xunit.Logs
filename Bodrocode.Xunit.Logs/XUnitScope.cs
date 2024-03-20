namespace Bodrocode.Xunit.Logs;

/// <summary>
///     An empty scope without any logic
/// </summary>
internal sealed class XUnitScope : IDisposable
{
    public static XUnitScope Instance { get; } = new();

    public void Dispose()
    {
    }
}