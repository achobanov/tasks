using Challenge.Common.Injection;

namespace Challenge.Common;

public interface INotifier : ISingleton
{
    Task Error(string message, string? details);
    Task Validation(string message, string? details);
}
