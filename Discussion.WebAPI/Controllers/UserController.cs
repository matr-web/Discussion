using Discussion.BLL.Services.Interfaces;
using Discussion.Models.DTO_s.UserDTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Discussion.WebAPI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("Get")]
    public async Task<ActionResult<UserDTO>> GetAsync([FromQuery] string? userNameOrEmail)
    {
        var userDTO = await _userService
            .GetUserByAsync(u => u.Username == userNameOrEmail || u.Email == userNameOrEmail, "Questions,Answers,Ratings");

        if (userDTO == null)
        {
            return NotFound();
        }

        return Ok(userDTO);
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> DeleteAsync([FromQuery] int userId)
    {
        var userDTO = await _userService.GetUserByAsync(u => u.Id == userId);

        if (userDTO == null)
        {
            return NotFound();
        }

        // Only User by himself and Administrator can Delete a User.
        if (!_userService.User.IsInRole("Administrator") && _userService.UserId != userDTO.Id)
        {
            return Forbid();
        }

        await _userService.DeleteUserAsync(userDTO.Id);

        return NoContent();
    }
}
