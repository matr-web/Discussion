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
    ClaimsPrincipal User { get; }

    /// <summary>
    /// Get User Id from current Context.
    /// </summary>
    int? UserId { get; }

    /// <summary>
    /// Register User and save him in the DB.
    /// </summary>
    /// <param name="registerUserDto">DTO that contains data for a User that should be added to the DB.</param>
    /// <returns>UserDTO type with the current Registered User.</returns>
    Task<UserDTO> RegisterUserAsync(RegisterUserDTO registerUserDto);

    /// <summary>
    /// Generate a new Token for a User that is logging in.
    /// </summary>
    /// <param name="userWithHashDTO">A User that we generate a token for.</param>
    /// <returns>Token which contains given User data.</returns>
    string GenerateToken(UserWithHashDTO userWithHashDTO);

    /// <summary>
    /// Get a specific User that fulfill given filterExpression.
    /// </summary>
    /// <param name="filterExpression">Requirement's that must be fulfilled by a User to be returned.</param>
    /// <param name="includeProperties">Optional related properties.</param>
    /// <returns>UserDTO type.</returns>
    Task<UserDTO> GetUserByAsync(Expression<Func<UserEntity, bool>> filterExpression, string includeProperties = null);

    /// <summary>
    /// Get a specific User with his hashed password that fulfill given filterExpression.
    /// </summary>
    /// <param name="filterExpression">Requirement's that must be fulfilled by a User to be returned.</param>
    /// <returns>UserWithHashDTO type.</returns>
    Task<UserWithHashDTO> GetUserWithHashByAsync(Expression<Func<UserEntity, bool>> filterExpression);

    /// <summary>
    /// Change User's Password.
    /// </summary>
    /// <param name="changeUserPasswordDTO">DTO that contains new Password data.</param>
    /// <returns>UserDTO type.</returns>
    Task<UserDTO> ChangePasswordAsync(ChangeUserPasswordDTO changeUserPasswordDTO);

    /// <summary>
    /// Delete given User from the DB.
    /// </summary>
    /// <param name="userId">Id of User that should be deleted.</param>
    Task DeleteUserAsync(int userId);
}
