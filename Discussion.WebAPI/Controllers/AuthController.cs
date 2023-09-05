using Discussion.BLL.Services.Interfaces;
using Discussion.Models.DTO_s.UserDTO_s;
using Microsoft.AspNetCore.Mvc;

namespace Discussion.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> RegisterAsync([FromBody] RegisterUserDTO registerUserDTO)
    {
        var userDTO = await _userService.RegisterUserAsync(registerUserDTO);

        return Created($"User/{userDTO.Id}", userDTO);
    }

    [HttpPost("Login")]
    public async Task<ActionResult> LoginAsync([FromBody] LoginUserDTO loginUserDTO)
    {
        var userDTO = await _userService.GetUserByAsync(u => u.Email == loginUserDTO.UsernameOrEmail
            || u.Username == loginUserDTO.UsernameOrEmail);

        if (userDTO != null && BCrypt.Net.BCrypt.Verify(loginUserDTO.Password, userDTO.PasswordHash))
        {
            var token = await _userService.GenerateToken(userDTO);

            return Ok(token);
        }

        return BadRequest("Wrong Username or Password.");
    }
}
