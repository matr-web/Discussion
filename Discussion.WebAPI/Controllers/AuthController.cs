using Discussion.BLL.Services.Interfaces;
using Discussion.Models.DTO_s.UserDTO_s;
using Microsoft.AspNetCore.Mvc;
using Discussion.Models.DTO_s.EmailDTO_s;

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

        if(userDTO != null)
        {
            return BadRequest("User with given User Name or Email already exists.");
        }

        var registeredUserDTO = await _userService.RegisterUserAsync(registerUserDTO);

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
            var token = await _userService.GenerateToken(userWithHashDTO);

            return Ok(token);
        }

        return BadRequest("Wrong Username or Password.");
    }
}
