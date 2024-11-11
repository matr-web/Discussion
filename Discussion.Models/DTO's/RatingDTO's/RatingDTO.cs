using Discussion.Models.DTO_s.AnswerDTO_s;
using Discussion.Models.DTO_s.QuestionDTO_s;
using Discussion.Models.DTO_s.UserDTO_s;

namespace Discussion.Models.DTO_s.RatingDTO_s;

/// <summary>
/// A Data Transfer Object for Rating type.
/// </summary>
public class RatingDTO
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Value.
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    /// Author Id.
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Author.
    /// </summary>
    public virtual UserDTO User { get; set; }

    /// <summary>
    /// Question Id.
    /// </summary>
    public int? QuestionId { get; set; }

    /// <summary>
    /// Question.
    /// </summary>
    public virtual QuestionDTO Question { get; set; }

    /// <summary>
    /// Answer Id.
    /// </summary>
    public int? AnswerId { get; set; }

    /// <summary>
    /// Answer.
    /// </summary>
    public virtual AnswerDTO Answer { get; set; }
}
