
namespace Challenge.Common;

internal class EventSubscription
{
    public static EventSubscription<T> Create<T>(Action<T> payload)
    {
        return new EventSubscription<T>(payload);
    }
}

internal class EventSubscription<T>
{
    private readonly Action<T> _action;

    internal EventSubscription(Action<T> action)
    {
        _action = action;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }

    public void Handle(T payload)
    {
        _action(payload);
    }
}
