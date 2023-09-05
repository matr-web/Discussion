using Discussion.Models.DTO_s.AnswerDTO_s;
using Discussion.Models.DTO_s.QuestionDTO_s;
using Discussion.Models.DTO_s.RatingDTO_s;
using System.ComponentModel.DataAnnotations;

namespace Discussion.Models.DTO_s.UserDTO_s;

/// <summary>
/// A Data Transfer Object for User type.
/// </summary>
public class UserDTO
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

    /// <summary>
    /// Collection of User's Question's.
    /// </summary>
    public virtual ICollection<QuestionDTO> Questions { get; set; }

    /// <summary>
    /// Collection of User's Answer's.
    /// </summary>
    public virtual ICollection<AnswerDTO> Answers { get; set; }

    /// <summary>
    /// Collection of User's Ratings's.
    /// </summary>
    public virtual ICollection<RatingDTO> Ratings { get; set; }

    public static UserDTO ToUserDTO(object obj)
    {
        Type type = obj.GetType();
        var properties = type.GetProperties();

        var userDTO = new UserDTO()
        {
            Id = (int)properties.FirstOrDefault(p => p.Name == "Id").GetValue(obj),
            Username = properties.FirstOrDefault(p => p.Name == "Username").GetValue(obj).ToString(),
            Email = properties.FirstOrDefault(p => p.Name == "Email").GetValue(obj).ToString(),
            Role = properties.FirstOrDefault(p => p.Name == "Role").GetValue(obj).ToString(),
            PasswordHash = properties.FirstOrDefault(p => p.Name == "PasswordHash").GetValue(obj).ToString()
        };

        return userDTO;
    }
}
