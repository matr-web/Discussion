using System.ComponentModel.DataAnnotations;

namespace Discussion.Models.DTO_s.UserDTO_s;

public class UserWithHashDTO
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// User Name.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Email.
    /// </summary>
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    /// <summary>
    /// Role.
    /// </summary>
    public string Role { get; set; }

    /// <summary>
    /// PasswordHash.
    /// </summary>
    public string PasswordHash { get; set; }
}
