using Discussion.BLL.Services.Interfaces;
using Discussion.Models.DTO_s.RatingDTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Discussion.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RatingController : ControllerBase
{
    private readonly IRatingService _ratingService;
    private readonly IUserService _userService;

    public RatingController(IRatingService ratingService, IUserService userService)
    {
        _ratingService = ratingService;
        _userService = userService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<RatingDTO>>> GetAllAsync(
        [FromQuery] bool onlyPositive, [FromQuery] int? userId, [FromQuery] int? questionId, [FromQuery] int? answerId)
    {
        // Get rating's based on required value.
        // Only positive or all.
        var ratingDTOs = onlyPositive == true ?
            await _ratingService.GetRatingsAsync(r =>  r.Value == 1) :
            await _ratingService.GetRatingsAsync();

        // Take only those that match given parameters. 
        if (userId != null)
        {
            ratingDTOs = ratingDTOs
           .Where(r => r.UserId == userId);
        }

        if (questionId != null)
        {
            ratingDTOs = ratingDTOs
           .Where(r => r.QuestionId == questionId);
        }

        if (answerId != null)
        {
            ratingDTOs = ratingDTOs
           .Where(r => r.AnswerId == answerId);
        }

        // Load related properties.
        ratingDTOs = await _ratingService
            .GetRatingsAsync(r => ratingDTOs.Select(dto => dto.Id).Contains(r.Id),
            includeProperties: "User,Question,Answer");

        if (ratingDTOs == null || ratingDTOs.Count() == 0)
        {
            return NotFound();
        }

        // Order them descending based on Value.
        ratingDTOs = ratingDTOs.OrderByDescending(r => r.Value);

        return Ok(ratingDTOs);
    }

    [HttpPost("Post")]
    [Authorize]
    public async Task<ActionResult> PostAsync([FromBody] CreateRatingDTO createRatingDTO)
    {
        var ratingDTO = await _ratingService.InsertRatingAsync(createRatingDTO, (int)_userService.UserId);

        return Created($"Rating/{ratingDTO.Id}", ratingDTO);
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> DeleteAsync([FromQuery] int ratingId)
    {
        var ratingDTO = await _ratingService.GetRatingByAsync(r => r.Id == ratingId);

        if (ratingDTO == null)
        {
            return NotFound();
        }

        await _ratingService.DeleteRatingAsync(ratingDTO.Id);

        return NoContent();
    }
}
