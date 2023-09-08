namespace Discussion.Models.DTO_s.AnswerDTO_s;

/// <summary>
/// A Data Transfer Object for Create operation of Answer type.
/// </summary>
public class CreateAnswerDTO
{
    /// <summary>
    /// Content.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Question Id.
    /// </summary>
    public int QuestionId { get; set; }
}
