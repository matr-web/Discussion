using Discussion.Models.DTO_s.RatingDTO_s;

namespace Discussion.Utility.Mappers;

/// <summary>
/// Holds all Mappers for type RatingDTO.
/// </summary>
public class RatingMapper
{
    /// <summary>
    /// Mapper: object type -> RatingDTO
    /// </summary
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
