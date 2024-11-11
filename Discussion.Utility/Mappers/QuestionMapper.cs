using Discussion.Models.DTO_s.QuestionDTO_s;

namespace Discussion.Utility.Mappers;

/// <summary>
/// Holds all Mappers for type QuestionDTO.
/// </summary>
public class QuestionMapper
{
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

    /// <summary>
    /// Create new PaginatedQuestionDTOs object and return it.
    /// </summary>
    public static PaginatedQuestionDTOs ToPaginatedQuestionsDTO(IEnumerable<QuestionDTO> questionDTOs, int currentPage, int pageCount)
    {
        return new PaginatedQuestionDTOs
        {
            Questions = questionDTOs,
            CurrentPage = currentPage,
            PageCount = pageCount
        };
    }
}
