using Discussion.Entities;
using Discussion.Models.DTO_s.UserDTO_s;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Discussion.BLL.Services.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Get User from current Context.
    /// </summary>
    ClaimsPrincipal? User { get; }

    /// <summary>
    /// Get User Id from current Context.
    /// </summary>
    int? UserId { get; }

    /// <summary>
    /// Get a specific User that fulfill given filterExpression.
    /// </summary>
    /// <param name="filterExpression">Requirement's that must be fulfilled by a User to be returned.</param>
    /// <param name="includeProperties">Optional related properties.</param>
    /// <returns>UserDTO type.</returns>
    Task<UserDTO> GetUserByAsync(Expression<Func<UserEntity, bool>> filterExpression, string includeProperties = null);

    /// <summary>
    /// Register User and save him in the DB.
    /// </summary>
    /// <param name="registerUserDto">DTO that contains data for a User that should be added to the DB.</param>
    /// <returns>UserDTO type with the current Registered User.</returns>
    Task<UserDTO> RegisterUserAsync(RegisterUserDTO registerUserDto);

    /// <summary>
    /// Generate a new Token for a User that is logging in.
    /// </summary>
    /// <param name="useDTO">A User that we generate a token for.</param>
    /// <returns>Token with contains given User data.</returns>
    Task<string> GenerateToken(UserDTO useDTO);
}
