using Challenge.Common;

namespace Challenge.Domain.Core;

public class Notifier
{
    public static Event<string> Event { get; } = new();

    public void Notify(string message)
    {
        Event.Emmit(message);
    }
}
