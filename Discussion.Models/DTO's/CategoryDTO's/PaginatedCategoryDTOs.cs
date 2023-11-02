namespace Discussion.Models.DTO_s.CategoryDTO_s;

/// <summary>
/// A Data Transfer Object for the pagination process.
/// </summary>
public class PaginatedCategoryDTOs
{
    /// <summary>
    /// Categories which are found on given page.
    /// </summary>
    public IEnumerable<CategoryDTO> Categories { get; set; }

    /// <summary>
    /// Page number on which  the user is located right now.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// The count of all pages.
    /// </summary>
    public int PageCount { get; set; }

    /// <summary>
    /// Create new PaginatedCategoryDTOs object and return it.
    /// </summary>
    public static PaginatedCategoryDTOs ToPaginatedCategoriesDTO(IEnumerable<CategoryDTO> categoryDTOs, int currentPage, int pageCount)
    {
        return new PaginatedCategoryDTOs
        {
            Categories = categoryDTOs,
            CurrentPage = currentPage,
            PageCount = pageCount
        };
    }
}
