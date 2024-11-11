using Discussion.Entities;
using Discussion.Models.DTO_s.QuestionDTO_s;

namespace Discussion.Models.DTO_s.CategoryDTO_s;

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
    public ICollection<QuestionDTO> Questions { get; set; }
}
