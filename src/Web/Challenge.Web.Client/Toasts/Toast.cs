namespace Challenge.Web.Client.Toasts;

public class Toast
{
    private readonly DateTimeOffset posted;

    public Toast(string title, string? message, UiColor color, int secondsToLive = 5)
    {
        Title = title;
        Message = message ?? string.Empty;
        Color = color;
        TimeToBurn = DateTimeOffset.Now.AddSeconds(secondsToLive);
        posted = DateTimeOffset.Now;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public string Title { get; }
    public string Message { get; }
    public UiColor Color { get; }
    public DateTimeOffset TimeToBurn { get; }

    public bool IsBurnt => TimeToBurn < DateTimeOffset.Now;
    public string ElapsedTimeText => $"{-ElapsedTime.Seconds} seconds ago";
    private TimeSpan ElapsedTime => posted - DateTimeOffset.Now;
}

public enum UiColor
{
    Primary,
    Secondary,
    Success,
    Danger,
    Warning,
}