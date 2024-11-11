// Ignore Spelling: Auth

using Discussion.BLL.Services.Interfaces;
using Discussion.Models.DTO_s.UserDTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Discussion.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;

    public AuthController(IUserService userService, IEmailService emailService)
    {
        _userService = userService;
        _emailService = emailService;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> RegisterAsync([FromBody] RegisterUserDTO registerUserDTO)
    {
        var userDTO = await _userService.GetUserByAsync(u => u.Username == registerUserDTO.Username || u.Email == registerUserDTO.Email);

        if (userDTO != null)
        {
            return BadRequest("User with given User Name or Email already exists.");
        }

        var registeredUserDTO = await _userService.RegisterUserAsync(registerUserDTO);

        if(registeredUserDTO == null) 
        { 
            return NotFound();
        }

        _emailService.SendRegistrationEmail(registeredUserDTO.Email);

        return Created($"User/{registeredUserDTO.Id}", registeredUserDTO);
    }

    [HttpPost("Login")]
    public async Task<ActionResult> LoginAsync([FromBody] LoginUserDTO loginUserDTO)
    {
        var userWithHashDTO = await _userService.GetUserWithHashByAsync(u => u.Email == loginUserDTO.UsernameOrEmail
            || u.Username == loginUserDTO.UsernameOrEmail);

        if (userWithHashDTO != null && BCrypt.Net.BCrypt.Verify(loginUserDTO.Password, userWithHashDTO.PasswordHash))
        {
            var token = _userService.GenerateToken(userWithHashDTO);

            return Ok(token);
        }

        return BadRequest("Wrong Username or Password.");
    }

    [Authorize]
    [HttpPost("ChangePassword")]
    public async Task<ActionResult> ChangePasswordAsync([FromBody] ChangeUserPasswordDTO changeUserPasswordDTO)
    {
        // Only User by himself can change his own Password.
        if (_userService.UserId != changeUserPasswordDTO.Id)
        {
            return Forbid();
        }

        var userWithHashDTO = await _userService.GetUserWithHashByAsync(u => u.Id == changeUserPasswordDTO.Id);

        if(BCrypt.Net.BCrypt.Verify(changeUserPasswordDTO.CurrentPassword, userWithHashDTO.PasswordHash))
        {
            var userDTO = await _userService.ChangePasswordAsync(changeUserPasswordDTO);

            if(userDTO == null)
            {
                return NotFound();
            }

            _emailService.SendPasswordChangeConfirmationEmail(userDTO.Email);

            return Ok("Password has been Updated successfully."); 
        }

        return BadRequest("Your current Password is different.");
    }
}
