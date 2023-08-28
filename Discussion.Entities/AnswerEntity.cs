using System.ComponentModel.DataAnnotations.Schema;

namespace Discussion.Entities;

/// <summary>
/// Answer Entity.
/// </summary>
public class AnswerEntity
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Content.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Date of Creation.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Question Id.
    /// </summary>
    public int QuestionId { get; set; }

    /// <summary>
    /// The Question to which the Answer was given.
    /// </summary>
    public virtual QuestionEntity Question { get; set; }

    /// <summary>
    /// Author Id.
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Author.
    /// </summary>
    public virtual UserEntity? User { get; set; }

    /// <summary>
    /// Collection of Ratings.
    /// </summary>
    public virtual ICollection<RatingEntity>? Ratings { get; set; }

    /// <summary>
    /// Calculated Sum value of Ratings.
    /// </summary>
    [NotMapped]
    public int RatingValue
    {
        get
        {
            return Ratings == null || Ratings.Count == 0
                ? 0 : Ratings.Select(r => r.Value).Sum();
        }
    }
}
