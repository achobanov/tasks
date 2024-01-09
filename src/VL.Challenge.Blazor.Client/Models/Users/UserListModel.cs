using VL.Challenge.Common.Users;

namespace VL.Challenge.Common.Models.Users;

public class UserListModel
{
    public string Username { get; init; } = default!;
    public int Tasks { get; init; }
}
