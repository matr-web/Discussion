using Discussion.Models.DTO_s.AnswerDTO_s;
using Discussion.Models.DTO_s.CategoryDTO_s;
using Discussion.Models.DTO_s.RatingDTO_s;
using Discussion.Models.DTO_s.UserDTO_s;

namespace Discussion.Models.DTO_s.QuestionDTO_s;

/// <summary>
/// A Data Transfer Object for Question type.
/// </summary>
public class QuestionDTO
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
    public CategoryDTO Category { get; set; }

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
    public virtual UserDTO? User { get; set; }

    /// <summary>
    /// Collection of Answers.
    /// </summary>
    public virtual ICollection<AnswerDTO>? Answers { get; set; }

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

    /// <summary>
    /// Mapper: object type -> QuestionDTO
    /// </summary
    public static QuestionDTO ToQuestionDTO(object obj)
    {
        Type type = obj.GetType();
        var properties = type.GetProperties();

        var questionDTO = new QuestionDTO
        {
            Id = (int)properties.FirstOrDefault(p => p.Name == "Id").GetValue(obj),
            CategoryId = (int)properties.FirstOrDefault(p => p.Name == "CategoryId").GetValue(obj),
            Topic = properties.FirstOrDefault(p => p.Name == "Topic").GetValue(obj).ToString(),
            Content = properties.FirstOrDefault(p => p.Name == "Content").GetValue(obj).ToString(),
            Date = Convert.ToDateTime(properties.FirstOrDefault(p => p.Name == "Date").GetValue(obj)),
            UserId = properties.FirstOrDefault(p => p.Name == "UserId").GetValue(obj) != null ?
            (int)properties.FirstOrDefault(p => p.Name == "UserId").GetValue(obj) : 0
        };

        return questionDTO;
    }
}
