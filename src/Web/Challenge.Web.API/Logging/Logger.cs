using Challenge.Common;
using System.Text;

namespace Challenge.Web.API.Logging;

public class Logger : INotifier
{
    public Task Error(string message, string? details)
    {
        Console.WriteLine(Format(nameof(Error), message, details));
        return Task.CompletedTask;
    }

    public Task Information(string message)
    {
        Console.WriteLine(Format(nameof(Information), message, null));
        return Task.CompletedTask;
    }

    public Task Validation(string message, string? details)
    {
        Console.WriteLine(Format(nameof(Validation), message, details));
        return Task.CompletedTask;
    }

    private string Format(string prefix, string message, string? details)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{DateTimeOffset.UtcNow}:{prefix}: {message}");
        if (details != null)
        {
            sb.AppendLine();
            sb.AppendLine(details);
        }
        return sb.ToString();
    }
}


