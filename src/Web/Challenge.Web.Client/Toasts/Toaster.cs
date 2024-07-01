using Challenge.Common;
using System.Timers;

namespace Challenge.Web.Client.Toasts;

public class Toaster : IToaster, IDisposable
{
    private readonly ToastCollection _toastCollection = new();
    private readonly System.Timers.Timer timer = new();

    public Toaster()
    {
        timer.Interval = 1000;
        timer.AutoReset = true;
        timer.Elapsed += HandleTimerElapsed;
        timer.Start();
    }

    public event EventHandler? ToasterChanged;

    public bool HasToasts => _toastCollection.HasToasts();

    public IEnumerable<Toast> GetToasts()
    {
        CheckBurnt();
        return _toastCollection.ToList();
    }

    public Task Error(string message, string? _)
    {
        Add("Unhandled error", message, UiColor.Danger);
        return Task.CompletedTask;
    }

    public Task Validation(string message, string? _)
    {
        Add("Validation error", message, UiColor.Warning);
        return Task.CompletedTask;
    }

    public void Add(string header, string message, UiColor color, int seconds = 10)
    {
        var toast = new Toast(header, message, color, seconds);
        _toastCollection.Add(toast);
        if (!CheckBurnt())
        {
            ToasterChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ClearToast(Toast toast)
    {
        _toastCollection.Remove(toast);
        if (!CheckBurnt())
        {
            ToasterChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Dispose()
    {
        if (timer is not null)
        {
            timer.Elapsed += HandleTimerElapsed;
            timer.Stop();
            timer.Dispose();
        }
    }

    private bool CheckBurnt()
    {
        var hadBurnt = _toastCollection.DiscardBurnt();
        if (hadBurnt)
        {
            ToasterChanged?.Invoke(this, EventArgs.Empty);
        }
        return hadBurnt;
    }

    private void HandleTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        CheckBurnt();
        ToasterChanged?.Invoke(this, EventArgs.Empty);
    }
}

public interface IToaster : INotifier
{
    void Add(string header, string message, UiColor color, int seconds = 10);
    IEnumerable<Toast> GetToasts();
    void ClearToast(Toast toast);
}