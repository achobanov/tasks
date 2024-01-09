namespace VL.Challenge.Blazor.Client.Services;

public class UserContext : IUserContext
{
    public int? LoggingId { get; set;  }
}

public interface IUserContext
{
    int? LoggingId { get; }
}
