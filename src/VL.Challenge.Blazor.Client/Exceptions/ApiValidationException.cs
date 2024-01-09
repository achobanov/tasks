namespace VL.Challenge.Blazor.Client.Exceptions;

public class ApiValidationException : ApplicationException
{
    public ApiValidationException(string message) : base(message)
    {
    }

    public string Title => "Invalid";
}
