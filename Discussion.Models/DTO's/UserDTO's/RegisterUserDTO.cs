using System.ComponentModel.DataAnnotations;

namespace Discussion.Models.DTO_s.UserDTO_s;

/// <summary>
/// A Data Transfer Object for Register operation for the Application User.
/// </summary>
public class RegisterUserDTO
{
    /// <summary>
    /// User Name.
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// Email.
    /// </summary>
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    [MinLength(6)]
    public required string Password { get; set; }

    /// <summary>
    /// Role.
    /// </summary>
    public required string Role { get; set; }
}
