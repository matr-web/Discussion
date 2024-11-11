using Discussion.BLL.Services.Interfaces;
using Discussion.Models.DTO_s.QuestionDTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Discussion.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionController : ControllerBase
{
    private readonly IQuestionService _questionService;
    private readonly IUserService _userService;

    public QuestionController(IQuestionService questionService, IUserService userService)
    {
        _questionService = questionService;
        _userService = userService;
    }

    [HttpGet("GetAll/{categoryId}")]
    public async Task<ActionResult<PaginatedQuestionDTOs>> GetAllAsync
        ([FromRoute]int categoryId, [FromQuery]string searchPhrase, [FromQuery]string orderByProperty = "Topic", [FromQuery]int currentPage = 1)
    {
        var questionDTOs = await _questionService
            .GetQuestionsAsync(orderByProperty, q => q.CategoryId == categoryId, includeProperties: "Category,User,Ratings");

        if(searchPhrase != null)
        {
            questionDTOs = questionDTOs.Where(q => q.Topic.ToLower().Contains(searchPhrase.ToLower()));
        }        

        return questionDTOs.Any() ? Ok(_questionService.PaginateQuestions(questionDTOs, currentPage, 15)) : NotFound();
    }

    [HttpGet("Get/{questionId}")]
    public async Task<ActionResult<QuestionDTO>> GetAsync([FromRoute] int questionId)
    {
        var questionDTO = await _questionService.GetQuestionByAsync(q => q.Id == questionId, "Category,User,Answers,Ratings");

        return questionDTO != null ? Ok(questionDTO) : NotFound();
    }

    [Authorize]
    [HttpPost("Post")]
    public async Task<ActionResult> PostAsync([FromBody] CreateQuestionDTO createQuestionDTO)
    {
        var questionDTO = await _questionService.InsertQuestionAsync(createQuestionDTO, (int)_userService.UserId);

        return Created($"Question/{questionDTO.Id}", questionDTO);
    }

    [Authorize]
    [HttpPut("Put/{questionId}")]
    public async Task<ActionResult> PutAsync([FromRoute] int questionId, [FromBody] UpdateQuestionDTO updateQuestionDTO)
    {
        if (questionId != updateQuestionDTO.Id)
        {
            return BadRequest();
        }

        // Only the Author can Update a Question.
        if (_userService.UserId != updateQuestionDTO.UserId)
        {
            return Forbid();
        }

        var questionDTO = await _questionService.UpdateQuestionAsync(updateQuestionDTO);

        return Ok(questionDTO);
    }

    [Authorize]
    [HttpDelete("Delete/{questionId}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int questionId)
    {        
        var questionDTO = await _questionService.GetQuestionByAsync(g => g.Id == questionId);

        if (questionDTO == null)
        {
            return NotFound();
        }

        // Only the Author and Administrator can Delete a Question.
        if (!_userService.User.IsInRole("Administrator") && _userService.UserId != questionDTO.UserId)
        {
            return Forbid();
        }

        await _questionService.DeleteQuestionAsync(questionId);

        return NoContent();
    }
}
