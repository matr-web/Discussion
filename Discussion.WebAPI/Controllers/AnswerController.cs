﻿using Discussion.BLL.Services.Interfaces;
using Discussion.Models.DTO_s.AnswerDTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Discussion.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnswerController : ControllerBase
{
    private readonly IAnswerService _answerService;
    private readonly IUserService _userService;

    public AnswerController(IAnswerService answerService, IUserService userService)
    {
        _answerService = answerService;
        _userService = userService;
    }

    [HttpGet("GetAll/{questionId}")]
    public async Task<ActionResult<IEnumerable<AnswerDTO>>> GetAllAsync([FromRoute] int questionId)
    {
        var answerDTOs = await _answerService.GetAnswersAsync(a => a.QuestionId == questionId, includeProperties: "User,Ratings");

        return answerDTOs.Any() ? Ok(answerDTOs) : NotFound();
    }

    [HttpGet("Get/{answerId}")]
    public async Task<ActionResult<AnswerDTO>> GetAsync([FromRoute] int answerId)
    {
        var answerDTO = await _answerService.GetAnswerByAsync(a => a.Id == answerId, "Question,User,Ratings");

        return answerDTO != null ? Ok(answerDTO) : NotFound();
    }

    [Authorize]
    [HttpPost("Post")]
    public async Task<ActionResult> PostAsync([FromBody] CreateAnswerDTO createAnswerDTO)
    {
        var answerDTO = await _answerService.InsertAnswerAsync(createAnswerDTO, (int)_userService.UserId);

        return Created($"Answer/{answerDTO.Id}", answerDTO);
    }

    [Authorize]
    [HttpPut("Put/{answerId}")]
    public async Task<ActionResult> PutAsync([FromRoute] int answerId, [FromBody] UpdateAnswerDTO updateAnswerDTO)
    {
        if (answerId != updateAnswerDTO.Id)
        {
            return BadRequest();
        }

        // Only the Author can Update a Answer.
        if (_userService.UserId != updateAnswerDTO.UserId)
        {
            return Forbid();
        }

        var answerDTO = await _answerService.UpdateAnswerAsync(updateAnswerDTO);

        return Ok(answerDTO);
    }

    [Authorize]
    [HttpDelete("Delete/{answerId}")]
    public async Task<ActionResult> DeleteAsync([FromQuery] int answerId)
    {
        var answerDTO = await _answerService.GetAnswerByAsync(g => g.Id == answerId);

        if (answerDTO == null)
        {
            return NotFound();
        }

        // Only the Author and Administrator can Delete a Answer.
        if (!_userService.User.IsInRole("Administrator") && _userService.UserId != answerDTO.UserId)
        {
            return Forbid();
        }

        await _answerService.DeleteAnswerAsync(answerDTO.Id);

        return NoContent();
    }
}
