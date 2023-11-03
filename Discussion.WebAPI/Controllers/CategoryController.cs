using Discussion.BLL.Services.Interfaces;
using Discussion.Models.DTO_s.CategoryDTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Discussion.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<PaginatedCategoryDTOs>> GetAllAsync([FromQuery] int currentPage = 1)
    {
        var categoryDTOs = await _categoryService.GetCategoriesAsync();

        if (categoryDTOs == null || categoryDTOs.Count() == 0)
        {
            return NotFound();
        }
        var paginatedCategoryDTOs = _categoryService.PaginateCategories(categoryDTOs, currentPage, 2);

        return Ok(paginatedCategoryDTOs);
    }

    [HttpGet("Get/{categoryId}")]
    public async Task<ActionResult<CategoryDTO>> GetAsync([FromRoute] int categoryId)
    {
        var categoryDTO = await _categoryService.GetCategoryByAsync(c => c.Id == categoryId, "Questions");

        if (categoryDTO == null)
        {
            return NotFound();
        }

        return Ok(categoryDTO);
    }

    [HttpPost("Post")]
    [Authorize(Roles =$"{StaticData.role_administrator}")]
    public async Task<ActionResult> PostAsync([FromBody] CreateCategoryDTO createCategoryDTO)
    {
        var categoryDTO = await _categoryService.InsertCategoryAsync(createCategoryDTO);

        return Created($"Category/{categoryDTO.Id}", categoryDTO);
    }

    [HttpPut("Put/{categoryId}")]
    [Authorize(Roles = $"{StaticData.role_administrator}")]
    public async Task<ActionResult> PutAsync([FromRoute] int categoryId, [FromBody] UpdateCategoryDTO updateCategoryDTO)
    {
        if (categoryId != updateCategoryDTO.Id)
        {
            return BadRequest();
        }

        var categoryDTO = await _categoryService.UpdateCategoryAsync(updateCategoryDTO);

        return Ok(categoryDTO);
    }

    [HttpDelete("Delete/{categoryId}")]
    [Authorize(Roles = $"{StaticData.role_administrator}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int categoryId)
    {
        var categoryDTO = await _categoryService.GetCategoryByAsync(g => g.Id == categoryId);

        if (categoryDTO == null)
        {
            return NotFound();
        }

        await _categoryService.DeleteCategoryAsync(categoryDTO.Id);

        return NoContent();
    }
}
