namespace Discussion.Models.DTO_s.QuestionDTO_s;

/// <summary>
/// A Data Transfer Object for the pagination process.
/// </summary>
public class PaginatedQuestionDTOs
{
    /// <summary>
    /// Question's which are found on given page.
    /// </summary>
    public IEnumerable<QuestionDTO> Questions { get; set; }

    /// <summary>
    /// Page number on which  the user is located right now.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// The count of all pages.
    /// </summary>
    public int PageCount { get; set; }

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
