namespace Discussion.Entities;

/// <summary>
/// Rating Entity.
/// </summary>
public class RatingEntity
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
    public virtual UserEntity? User { get; set; }

    /// <summary>
    /// Question Id.
    /// </summary>
    public int? QuestionId { get; set; }

    /// <summary>
    /// Question.
    /// </summary>
    public virtual QuestionEntity? Question { get; set; }

    /// <summary>
    /// Answer Id.
    /// </summary>
    public int? AnswerId { get; set; }

    /// <summary>
    /// Answer.
    /// </summary>
    public virtual AnswerEntity? Answer { get; set; }
}
