using Challenge.Common;

namespace Challenge.Tests.Mocks;

internal class TestNotifier : INotifier
{
    public Task Error(string message, string? details = null)
    {
        return Task.CompletedTask;
    }

    public Task Validation(string message, string? details = null)
    {
        return Task.CompletedTask;
    }
}
