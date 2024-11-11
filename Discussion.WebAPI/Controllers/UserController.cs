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
    private readonly IEmailService _emailService;

    public UserController(IUserService userService, IEmailService emailService)
    {
        _userService = userService;
        _emailService = emailService;
    }

    [HttpGet("Get/{userNameOrEmail}")]
    public async Task<ActionResult<UserDTO>> GetAsync([FromRoute] string userNameOrEmail)
    {
        var userDTO = await _userService
            .GetUserByAsync(u => u.Username == userNameOrEmail || u.Email == userNameOrEmail, "Questions,Answers,Ratings");

        return userDTO != null ? Ok(userDTO) : NotFound();
    }

    [HttpDelete("Delete/{userId}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int userId)
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

        await _userService.DeleteUserAsync(userId);

        _emailService.SendAccountDeleteEmail(userDTO.Email);

        return NoContent();
    }
}
