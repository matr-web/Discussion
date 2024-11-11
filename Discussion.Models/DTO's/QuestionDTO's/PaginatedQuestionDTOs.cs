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
}
