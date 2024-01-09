using System.ComponentModel.DataAnnotations;

namespace VL.Challenge.Common.Users;

public class UserCreateModel
{
    [Required]
    [MinLength(3, ErrorMessage = "Username needs to be at least 3 sybols long")]
    public string Username { get; set; } = default!;
}
