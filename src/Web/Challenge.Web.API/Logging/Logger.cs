using Challenge.Common;
using System.Text;

namespace Challenge.Web.API.Logging;

public class Logger : INotifier
{
    public Task Error(string message, string? details)
    {
        Console.WriteLine(Format(message, details));
        return Task.CompletedTask;
    }

    public Task Validation(string message, string? details)
    {
        Console.WriteLine(Format(message, details));
        return Task.CompletedTask;
    }

    private string Format(string message, string? details)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{DateTimeOffset.UtcNow}: {message}");
        if (details != null)
        {
            sb.AppendLine();
            sb.AppendLine(details);
        }
        return sb.ToString();
    }
}


