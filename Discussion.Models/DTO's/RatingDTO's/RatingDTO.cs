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
    public virtual UserDTO? User { get; set; }

    /// <summary>
    /// Question Id.
    /// </summary>
    public int? QuestionId { get; set; }

    /// <summary>
    /// Question.
    /// </summary>
    public virtual QuestionDTO? Question { get; set; }

    /// <summary>
    /// Answer Id.
    /// </summary>
    public int? AnswerId { get; set; }

    /// <summary>
    /// Answer.
    /// </summary>
    public virtual AnswerDTO? Answer { get; set; }

    public static RatingDTO ToRatingDTO(object obj)
    {
        Type type = obj.GetType();
        var properties = type.GetProperties();

        var ratingDTO = new RatingDTO
        {
            Id = (int)properties.FirstOrDefault(p => p.Name == "Id").GetValue(obj),
            Value = (int)properties.FirstOrDefault(p => p.Name == "Value").GetValue(obj),
            UserId = properties.FirstOrDefault(p => p.Name == "UserId").GetValue(obj) != null ?
            (int)properties.FirstOrDefault(p => p.Name == "UserId").GetValue(obj) : 0,
            QuestionId = properties.FirstOrDefault(p => p.Name == "QuestionId").GetValue(obj) != null ?
            (int)properties.FirstOrDefault(p => p.Name == "QuestionId").GetValue(obj) : 0,
            AnswerId = properties.FirstOrDefault(p => p.Name == "AnswerId").GetValue(obj) != null ?
            (int)properties.FirstOrDefault(p => p.Name == "AnswerId").GetValue(obj) : 0
        };

        return ratingDTO;
    }
}
