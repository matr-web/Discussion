namespace Discussion.Models.DTO_s.QuestionDTO_s;

/// <summary>
/// A Data Transfer Object for Create operation of Question type.
/// </summary>
public class CreateQuestionDTO
{
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

    /// <summary>
    /// Author Id.
    /// </summary>
    public int UserId { get; set; }
}
