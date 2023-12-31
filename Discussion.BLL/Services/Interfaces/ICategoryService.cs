﻿using Discussion.Entities;
using Discussion.Models.DTO_s.CategoryDTO_s;
using Discussion.Models.DTO_s.QuestionDTO_s;
using System.Linq.Expressions;

namespace Discussion.BLL.Services.Interfaces;

public interface ICategoryService
{
    /// <summary>
    /// Get all Categories that fulfill given filterExpression if it is given. If not Get all Categories.
    /// </summary>
    /// <param name="filterExpression">Optional requirement's that must be fulfilled if given.</param>
    /// <param name="includeProperties">Optional related properties.</param>
    /// <returns>Collection of CategoryDTO type.</returns>
    Task<IEnumerable<CategoryDTO>> GetCategoriesAsync(Expression<Func<CategoryEntity, bool>> filterExpression = null, string includeProperties = null);

    /// <summary>
    /// Get a specific Category that fulfill given filterExpression.
    /// </summary>
    /// <param name="filterExpression">Requirement's that must be fulfilled by a Category to be returned.</param>
    /// <param name="includeProperties">Optional related properties.</param>
    /// <returns>CategoryDTO type.</returns>
    Task<CategoryDTO> GetCategoryByAsync(Expression<Func<CategoryEntity, bool>> filterExpression, string includeProperties = null);

    /// <summary>
    /// Insert new Category type to the DB.
    /// </summary>
    /// <param name="createCategoryDTO">DTO that contains data for a Category that should be added to the DB.</param>
    /// <returns>CategoryDTO type with the just created Category data.</returns>
    Task<CategoryDTO> InsertCategoryAsync(CreateCategoryDTO createCategoryDTO);

    /// <summary>
    /// Update given Category in the DB.
    /// </summary>
    /// <param name="updateCategoryDTO">DTO that contains data for a Category that should be updated in the DB.</param>
    /// <returns>CategoryDTO type with the just updated Category data.</returns>
    Task<CategoryDTO> UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO);

    /// <summary>
    /// Delete given Category from the DB.
    /// </summary>
    /// <param name="categoryId">Id of Category that should be deleted.</param>
    Task DeleteCategoryAsync(int categoryId);

    /// <summary>
    /// Paginate CategoryDTO's.
    /// </summary>
    /// <param name="categoryDTOs">Collection of CategoryDTO's that will be paginated.</param>
    /// <param name="currentPage">Current page value.</param>
    /// <param name="pageSize">The size of each page.</param>
    /// <returns>Collection of CategoryDTO's that should be on the given page. Current page number and the over all count of pages.</returns>
    PaginatedCategoryDTOs PaginateCategories(IEnumerable<CategoryDTO> categoryDTOs, int currentPage, int pageSize);
}
