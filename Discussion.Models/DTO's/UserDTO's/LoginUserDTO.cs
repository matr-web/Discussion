using System.ComponentModel.DataAnnotations;

namespace Discussion.Models.DTO_s.UserDTO_s;

public class LoginUserDTO
{
    /// <summary>
    /// User name or Email.
    /// </summary>
    public string UsernameOrEmail { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    [MinLength(6)]
    public string Password { get; set; }
}
