using Discussion.Models.DTO_s.QuestionDTO_s;
using Discussion.Models.DTO_s.RatingDTO_s;
using Discussion.Models.DTO_s.UserDTO_s;

namespace Discussion.Models.DTO_s.AnswerDTO_s;

/// <summary>
/// A Data Transfer Object for Answer type.
/// </summary>
public class AnswerDTO
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
    public virtual QuestionDTO Question { get; set; }

    /// <summary>
    /// Author Id.
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Author.
    /// </summary>
    public virtual UserDTO? User { get; set; }

    /// <summary>
    /// Collection of Ratings.
    /// </summary>
    public virtual ICollection<RatingDTO>? Ratings { get; set; }

    /// <summary>
    /// Calculated Sum value of Ratings.
    /// </summary>
    public int RatingValue
    {
        get
        {
            return Ratings == null || Ratings.Count == 0
                ? 0 : Ratings.Select(r => r.Value).Sum();
        }
    }

    public static AnswerDTO ToAnswerDTO(object obj)
    {
        Type type = obj.GetType();
        var properties = type.GetProperties();

        var answerDTO = new AnswerDTO()
        {
            Id = (int)properties.FirstOrDefault(p => p.Name == "Id").GetValue(obj),
            Content = properties.FirstOrDefault(p => p.Name == "Content").GetValue(obj).ToString(),
            Date = Convert.ToDateTime(properties.FirstOrDefault(p => p.Name == "Date").GetValue(obj)),
            QuestionId = (int)properties.FirstOrDefault(p => p.Name == "QuestionId").GetValue(obj),
            UserId = properties.FirstOrDefault(p => p.Name == "UserId").GetValue(obj) != null ?
            (int)properties.FirstOrDefault(p => p.Name == "UserId").GetValue(obj) : 0,
        };

        return answerDTO;
    }
}
