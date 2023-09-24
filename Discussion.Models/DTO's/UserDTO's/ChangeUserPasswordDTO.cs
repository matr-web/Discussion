using System.ComponentModel.DataAnnotations;

namespace Discussion.Models.DTO_s.UserDTO_s;

/// <summary>
/// A Data Transfer Object for Password change operation for the Application User.
/// </summary>
public class ChangeUserPasswordDTO
{
    /// <summary>
    /// User Id whose Password will be changed.
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Current Password.
    /// </summary>
    [Required]
    public string CurrentPassword { get; set; }

    /// <summary>
    /// New Password.
    /// </summary>
    [Required]
    [MinLength(6)]
    public string NewPassword { get; set; }

    /// <summary>
    /// Confirm Password.
    /// </summary>
    [Required]
    [Compare("NewPassword", ErrorMessage = "Given Password's doesn't match.")]
    public string ConfirmPassword { get; set; }
}
