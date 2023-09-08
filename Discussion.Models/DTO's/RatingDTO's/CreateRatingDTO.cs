namespace Discussion.Models.DTO_s.RatingDTO_s;

/// <summary>
/// A Data Transfer Object for Create operation of Rating type.
/// </summary>
public class CreateRatingDTO
{
    /// <summary>
    /// Value.
    /// </summary>
    public int Value { get; set; }

    /// s<summary>
    /// QuetionId.
    /// </summary>
    public int? QuestionId { get; set; }

    /// <summary>
    /// AnswerId.
    /// </summary>
    public int? AnswerId { get; set; }
}