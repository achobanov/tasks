using Challenge.Common.Injection;

namespace Challenge.Common;

public interface INotifier : ISingleton
{
    Task Information(string message);
    Task Error(string message, string? details = null);
    Task Validation(string message, string? details = null);
}
