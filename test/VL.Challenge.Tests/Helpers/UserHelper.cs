using VL.Challenge.Domain.Entities;

namespace VL.Challenge.Domain.Tests.Helpers;
internal static class UserHelper
{
    public static User Create(int? id = null, string? username = null)
    {
        return new User(id ?? 1337, username ?? "username");
    }
}
