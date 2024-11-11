using Discussion.Models.DTO_s.UserDTO_s;

namespace Discussion.Utility.Mappers;

/// <summary>
/// Holds all Mappers for type RatingDTO.
/// </summary>
public class UserMapper
{
    /// <summary>
    /// Mapper: object type -> UserDTO
    /// </summary
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
        };

        return userDTO;
    }

    /// <summary>
    /// Mapper: object type -> UserWithHashDTO
    /// </summary
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
