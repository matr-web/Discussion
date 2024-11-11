using Discussion.Models.DTO_s.CategoryDTO_s;

namespace Discussion.Utility.Mappers;

/// <summary>
/// Holds all Mappers for type CategoryDTO.
/// </summary>
public class CategoryMapper
{
    /// <summary>
    /// Mapper: object type -> CategoryDTO
    /// </summary
    public static CategoryDTO ToCategoryDTO(object obj)
    {
        Type type = obj.GetType();
        var properties = type.GetProperties();

        return new CategoryDTO
        {
            Id = (int)properties.FirstOrDefault(p => p.Name == "Id").GetValue(obj),
            Name = properties.FirstOrDefault(p => p.Name == "Name").GetValue(obj).ToString(),
        };
    }

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
