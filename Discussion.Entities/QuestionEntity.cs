using System.ComponentModel.DataAnnotations.Schema;

namespace Discussion.Entities;

/// <summary>
/// Question Entity.
/// </summary>
public class QuestionEntity
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
    /// Category.
    /// </summary>
    public CategoryEntity Category { get; set; }

    /// <summary>
    /// Topic.
    /// </summary>
    public string Topic { get; set; }

    /// <summary>
    /// Content.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Date of Creation.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Author Id.
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Author.
    /// </summary>
    public virtual UserEntity? User { get; set; }

    /// <summary>
    /// Collection of Answers.
    /// </summary>
    public virtual ICollection<AnswerEntity>? Answers { get; set; }

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

