using Microsoft.AspNetCore.Components.Web;

namespace Challenge.Web.Client.Components;

public class CustomBoundary : ErrorBoundary
{
    public string? Message => CurrentException?.Message;
    public string? StackTrace => CurrentException?.StackTrace;
}