using Discussion.Models.DTO_s.CategoryDTO_s;
namespace Discussion.Models.DTO_s.QuestionDTO_s;

/// <summary>
/// A Data Transfer Object for Update operation of Question type.
/// </summary>
public class UpdateQuestionDTO
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Category Id.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Topic.
    /// </summary>
    public string Topic { get; set; }

    /// <summary>
    /// Content.
    /// </summary>
    public string Content { get; set; }
}
