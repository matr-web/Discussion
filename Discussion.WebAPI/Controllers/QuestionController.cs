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

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetAllAsync()
    {
        var questionDTOs = await _questionService.GetQuestionsAsync(includeProperties: "Category,User,Ratings");

        if (questionDTOs == null || questionDTOs.Count() == 0)
        {
            return NotFound();
        }

        return Ok(questionDTOs);
    }

    [HttpGet("Get")]
    public async Task<ActionResult<QuestionDTO>> GetAsync([FromQuery] int questionId)
    {
        var questionDTO = await _questionService.GetQuestionByAsync(q => q.Id == questionId, "Category,User,Answers,Ratings");

        if (questionDTO == null)
        {
            return NotFound();
        }

        return Ok(questionDTO);
    }

    [Authorize]
    [HttpPost("Post")]
    public async Task<ActionResult> PostAsync([FromBody] CreateQuestionDTO createQuestionDTO)
    {
        var questionDTO = await _questionService.InsertQuestionAsync(createQuestionDTO, (int)_userService.UserId);

        return Created($"Question/{questionDTO.Id}", questionDTO);
    }

    [Authorize]
    [HttpPut("Put")]
    public async Task<ActionResult> PutAsync([FromQuery] int questionId, [FromBody] UpdateQuestionDTO updateQuestionDTO)
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
    [HttpDelete("Delete")]
    public async Task<ActionResult> DeleteAsync([FromQuery] int questionId)
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

        await _questionService.DeleteQuestionAsync(questionDTO.Id);

        return NoContent();
    }
}
