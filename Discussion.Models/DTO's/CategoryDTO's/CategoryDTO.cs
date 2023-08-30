using Discussion.Entities;
using Discussion.Models.DTO_s.QuestionDTO_s;

namespace Discussion.Models.DTO_s.CategoryDTO;

/// <summary>
/// A Data Transfer Object for Category type.
/// </summary>
public class CategoryDTO
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Collection of Questions of given Category.
    /// </summary>
    public ICollection<QuestionDTO>? Questions { get; set; }

    /// <summary>
    /// Mapper: object type -> CategoryDTO
    /// </summary
    public static CategoryDTO ToCategoryDTO(object obj)
    {
        Type type = obj.GetType();
        var properties = type.GetProperties();

        return new CategoryDTO
        {
            Id = (int)properties.FirstOrDefault(p => p.Name == "Id").GetValue(obj),
            Name = properties.FirstOrDefault(p => p.Name == "Name").GetValue(obj).ToString(),
        };
    }
}
