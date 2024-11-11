using Discussion.Models.DTO_s.AnswerDTO_s;

namespace Discussion.Utility.Mappers;

/// <summary>
/// Holds all Mappers for type AnswerDTO.
/// </summary>
public class AnswerMapper
{
    /// <summary>
    /// Mapper: object type -> AnswerDTO
    /// </summary
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
