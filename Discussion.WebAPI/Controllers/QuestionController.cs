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

    [HttpPost("Post")]
    [Authorize]
    public async Task<ActionResult> PostAsync([FromBody] CreateQuestionDTO createQuestionDTO)
    {
        var questionDTO = await _questionService.InsertQuestionAsync(createQuestionDTO, (int)_userService.UserId);

        return Created($"Question/{questionDTO.Id}", questionDTO);
    }

    [HttpPut("Put")]
    public async Task<ActionResult> PutAsync([FromQuery] int questionId, [FromBody] UpdateQuestionDTO updateQuestionDTO)
    {
        if (questionId != updateQuestionDTO.Id)
        {
            return BadRequest();
        }

        var questionDTO = await _questionService.UpdateQuestionAsync(updateQuestionDTO);

        return Ok(questionDTO);
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> DeleteAsync([FromQuery] int questionId)
    {
        var questionDTO = await _questionService.GetQuestionByAsync(g => g.Id == questionId);

        if (questionDTO == null)
        {
            return NotFound();
        }

        await _questionService.DeleteQuestionAsync(questionDTO.Id);

        return NoContent();
    }
}
