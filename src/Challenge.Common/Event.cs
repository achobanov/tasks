namespace Challenge.Common;

public class Event<T> : IDisposable
{
    private readonly Dictionary<Guid, EventSubscription<T>> _subscritions = new();
    private Handler<T>? _handler;

    public Guid Subscribe(Action<T> action)
    {
        var subscription = EventSubscription.Create(action);
        _subscritions.Add(subscription.Id, subscription);
        _handler += subscription.Handle;
        return subscription.Id;
    }

    public void Unsubscribe(Guid id)
    {
        if (!_subscritions.ContainsKey(id))
        {
            return;
        }
        _subscritions.Remove(id);
    }

    public void Emmit(T payload)
    {
        _handler?.Invoke(payload);
    }

    public void Dispose()
    {
        foreach (var subscription in _subscritions.Values)
        {
            _handler -= subscription.Handle;
        }
    }
}

internal delegate void Handler<T>(T payload);


