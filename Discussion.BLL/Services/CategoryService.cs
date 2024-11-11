using Discussion.BLL.Services.Interfaces;
using Discussion.DAL.Repository.UnitOfWork;
using Discussion.Entities;
using Discussion.Models.DTO_s.CategoryDTO_s;
using Discussion.Utility.Mappers;
using System.Linq.Expressions;

namespace Discussion.BLL.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync(Expression<Func<CategoryEntity, bool>> filterExpression = null, string includeProperties = null)
    {
        // Get all needed Category Entities with fulfill given requirements.
        var categoryEntityCollection = await _unitOfWork.CategoryRepository.GetAllAsync(filterExpression, includeProperties);

        // If there are no Categories, return null.
        if (!categoryEntityCollection.Any())
        {
            return null;
        }

        // Else map them to dto's and return ordered by the Name property.
        return categoryEntityCollection.Select(MapToCategoryDTO).OrderBy(c => c.Name);
    }

    public async Task<CategoryDTO> GetCategoryByAsync(Expression<Func<CategoryEntity, bool>> filterExpression, string includeProperties = null)
    {
        // Get CategoryEntity with fulfill given requirements.
        var categoryEntity = await _unitOfWork.CategoryRepository.GetAsync(filterExpression, includeProperties);

        // If no such Category exists - return null.
        if (categoryEntity == null)
        {
            return null;
        }

        // Map it to DTO and return.
        return MapToCategoryDTO(categoryEntity);
    }

    public async Task<CategoryDTO> InsertCategoryAsync(CreateCategoryDTO createCategoryDTO)
    {
        // Create new CategoryEntity with given data.
        var categoryEntity = new CategoryEntity
        {
            Name = createCategoryDTO.Name
        };

        // Add it...
        await _unitOfWork.CategoryRepository.AddAsync(categoryEntity);
        await _unitOfWork.SaveAsync();

        // Map it to DTO and return.
        return MapToCategoryDTO(categoryEntity);
    }

    public async Task<CategoryDTO> UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO)
    {
        // Get CategoryEntity that will be updated.
        var categoryEntity = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == updateCategoryDTO.Id, "Questions");

        // Change the value's of updated properties.
        categoryEntity.Name = updateCategoryDTO.Name;   

        // Update it...
        await _unitOfWork.CategoryRepository.UpdateAsync(categoryEntity);
        await _unitOfWork.SaveAsync();

        // Map it to DTO and return.
        return MapToCategoryDTO(categoryEntity);
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        // Get CategoryEntity that should be deleted.
        var categoryEntity = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == categoryId);

        // Delete it...
        await _unitOfWork.CategoryRepository.Remove(categoryEntity);
        await _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Helper responsible for the correct course of mapping process between CategoryEntity and CategoryDTO.
    /// Checks if entities have some related data. If yes - map them too.
    /// Saves time because You don't have to do all the work manually every time You want to map Entity to DTO.
    /// </summary>
    /// <param name="categoryEntity">CategoryEntity element that will be mapped to DTO.</param>
    /// <returns>CategoryDTO element with data from given CategoryEntity as parameter.</returns>
    private static CategoryDTO MapToCategoryDTO(CategoryEntity categoryEntity)
    {
        var categoryDTO = CategoryMapper.ToCategoryDTO(categoryEntity);

        // Check if loaded Category Entity has related data - Collection of Question type.
        if (categoryEntity.Questions != null && categoryEntity.Questions.Count != 0)
        {
            // If yes - map each Question to DTO and add to the QuestionDTO collection located in CategoryDTO.
            categoryDTO.Questions = categoryEntity.Questions.Select(QuestionMapper.ToQuestionDTO).ToList();
        }

        return categoryDTO;
    }

    public PaginatedCategoryDTOs PaginateCategories(IEnumerable<CategoryDTO> categoryDTOs, int currentPage, int pageSize)
    {
        // Get the count of all categories.
        var cateogoriesCount = categoryDTOs.Count();

        // Calculate the total pages count.
        var totalPages = (int)Math.Ceiling((decimal)cateogoriesCount / pageSize);

        // Get the element's on given page.
        categoryDTOs = categoryDTOs
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var paginatedCategoriesDTOs = CategoryMapper.ToPaginatedCategoriesDTO(categoryDTOs, currentPage, totalPages);

        return paginatedCategoriesDTOs;
    }
}
