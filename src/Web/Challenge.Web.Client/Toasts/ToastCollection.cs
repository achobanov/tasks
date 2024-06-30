using System.Collections;

namespace Challenge.Web.Client.Toasts;

public class ToastCollection : IEnumerable<Toast>
{
    private readonly List<Toast> _toasts;
    private readonly object _lock = new();

    public ToastCollection() : this([])
    {
    }
    public ToastCollection(IEnumerable<Toast> toasts)
    {
        _toasts = toasts.ToList();
    }

    public void Add(Toast toast)
    {
        lock(_lock)
        {
            _toasts.Add(toast);
        }
    }

    public void Remove(Toast toast)
    {
        lock (_lock)
        {
            _toasts.Remove(toast);
        }
    }

    public bool DiscardBurnt()
    {
        var toasterChanged = false;
        lock (_lock)
        {
            foreach (var toast in _toasts.Where(x => x.IsBurnt))
            {
                _toasts.Remove(toast);
                toasterChanged = true;
            }
        }
        return toasterChanged;
    }

    public bool HasToasts()
    {
        return _toasts.Count > 0;
    }

    public IEnumerator<Toast> GetEnumerator()
    {
        return Enumerate();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return Enumerate();
    }

    private IEnumerator<Toast> Enumerate()
    {
        lock (_lock)
        {
            return _toasts.GetEnumerator();
        }
    }
}
