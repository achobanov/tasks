using System.Timers;

namespace VL.Challenge.Blazor.Client.Services;

public class Toaster : IToaster, IDisposable
{
    private readonly List<Toast> toastList = new();
    private readonly System.Timers.Timer timer = new();

    public Toaster()
    {
        this.timer.Interval = 1000;
        this.timer.AutoReset = true;
        this.timer.Elapsed += this.HandleTimerElapsed;
        this.timer.Start();
    }

    public event EventHandler? ToasterChanged;
    public event EventHandler? ToasterTimerElapsed;

    public bool HasToasts => toastList.Count > 0;

    public List<Toast> GetToasts()
    {
        ClearBurntToast();
        return this.toastList.ToList();
    }

    public void Add(string header, string? message, UiColor color, int seconds = 10)
    {
        var toast = new Toast(header, message, color, seconds);
        this.Add(toast);
    }

    public void Add(Toast toast)
    {
        this.toastList.Add(toast);
        if (!this.ClearBurntToast())
        {
            this.ToasterChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ClearToast(Toast toast)
    {
        if (this.toastList.Contains(toast))
        {
            this.toastList.Remove(toast);
            if (!this.ClearBurntToast())
            {
                this.ToasterChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void Dispose()
    {
        if (this.timer is not null)
        {
            this.timer.Elapsed += this.HandleTimerElapsed;
            this.timer.Stop();
        }
    }

    private bool ClearBurntToast()
    {
        var toastsToDelete = this.toastList.Where(item => item.IsBurnt).ToList();
        if (!toastsToDelete.Any())
        {
            return false;
        }

        toastsToDelete.ForEach(toast => this.toastList.Remove(toast));
        this.ToasterChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    private void HandleTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        this.ClearBurntToast();
        this.ToasterTimerElapsed?.Invoke(this, EventArgs.Empty);
    }

    public void Error(string message)
    {
        this.Add("Error", message, UiColor.Danger, 10);
    }
}

public interface IToaster
{
    void Add(string header, string? message, UiColor color, int seconds = 10);
    List<Toast> GetToasts();
    void ClearToast(Toast toast);
}

public class Toast
{
    private readonly DateTimeOffset posted;
    public Toast(string title, string? message, UiColor color, int secondsToLive = 5)
    {
        this.Title = title;
        this.Message = message ?? string.Empty;
        this.Color = color;
        this.TimeToBurn = DateTimeOffset.Now.AddSeconds(secondsToLive);
        this.posted = DateTimeOffset.Now;
        this.Id = Guid.NewGuid();
    }

    public Guid Id;
    public string Title { get; }
    public string Message { get; }
    public UiColor Color { get; }
    public DateTimeOffset TimeToBurn { get; }

    public bool IsBurnt
        => TimeToBurn < DateTimeOffset.Now;
    public string ElapsedTimeText
        => $"{-this.ElapsedTime.Seconds} seconds ago";

    private TimeSpan ElapsedTime
        => this.posted - DateTimeOffset.Now;
}
public enum UiColor
{
    Primary,
    Secondary,
    Success,
    Danger,
    Warning,
}
