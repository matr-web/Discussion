using Discussion.BLL.Services.Interfaces;
using Discussion.DAL.Repository.UnitOfWork;
using Discussion.Entities;
using Discussion.Models.DTO_s.CategoryDTO_s;
using Discussion.Models.DTO_s.QuestionDTO_s;
using System.Linq.Expressions;

namespace Discussion.BLL.Services;

internal class CategoryService : ICategoryService
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

        // Map from CategoryEntity to CategoryDTO collection.
        var categoryDTOList = new List<CategoryDTO>();

        foreach (var categoryEntity in categoryEntityCollection)
        {
            var categoryDTO = MapToCategoryDTO(categoryEntity);

            categoryDTOList.Add(categoryDTO);
        }

        // Return Collection of CategoryDTO type.
        return categoryDTOList; 
    }

    public async Task<CategoryDTO> GetCategoryByAsync(Expression<Func<CategoryEntity, bool>> filterExpression, string includeProperties = null)
    {
        // Get CategoryEntity with fulfill given requirements.
        var categoryEntity = await _unitOfWork.CategoryRepository.GetAsync(filterExpression, includeProperties);

        // Map it to DTO.
        var categoryDTO = MapToCategoryDTO(categoryEntity);

        // Return CategoryDTO with mapped CategoryEntity data.
        return categoryDTO;
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

        // Map it to DTO.
        var categoryDTO = MapToCategoryDTO(categoryEntity);

        // Return current inserted Entity mapped to DTO.
        return categoryDTO;
    }

    public async Task<CategoryDTO> UpdateCategoryAsync(CategoryDTO updateCategoryDTO)
    {
        // Get CategoryEntity that will be updated.
        var categoryEntity = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == updateCategoryDTO.Id, "Questions");

        // Change the value's of updated properties.
        categoryEntity.Name = updateCategoryDTO.Name;   

        // Update it...
        await _unitOfWork.CategoryRepository.UpdateAsync(categoryEntity);
        await _unitOfWork.SaveAsync();

        // Map it to DTO.
        var categoryDTO = MapToCategoryDTO(categoryEntity);

        // Return current mapped Entity as DTO.
        return categoryDTO;
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
    private CategoryDTO MapToCategoryDTO(CategoryEntity categoryEntity)
    {
        var categoryDTO = CategoryDTO.ToCategoryDTO(categoryEntity);

        // Check if loaded Category Entity has related data - Collection of Question type.
        if (categoryEntity.Questions != null && categoryEntity.Questions.Count() != 0)
        {
            // If yes - map each to DTO and add to the QuestionDTO collection located in CategoryDTO.
            foreach (var questionEntity in categoryEntity.Questions)
            {
                categoryDTO.Questions.Add(QuestionDTO.ToQuestionDTO(questionEntity));
            }
        }

        return categoryDTO;
    }
}
