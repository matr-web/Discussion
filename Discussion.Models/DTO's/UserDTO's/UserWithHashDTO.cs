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

    public static UserWithHashDTO ToUserWithHashDTO(object obj)
    {
        Type type = obj.GetType();
        var properties = type.GetProperties();

        var userDTO = new UserWithHashDTO()
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
