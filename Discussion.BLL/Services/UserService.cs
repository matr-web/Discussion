using Discussion.BLL.Services.Interfaces;
using Discussion.DAL.Repository.UnitOfWork;
using Discussion.Entities;
using Discussion.Models.DTO_s.UserDTO_s;
using System.Linq.Expressions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Discussion.Models.Models;
using Microsoft.Extensions.Logging;
using Discussion.Utility.Mappers;

namespace Discussion.BLL.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AuthenticationSettings _authenticationSettings;
    private readonly ILogger<UserService> _logger;

    public UserService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,
        AuthenticationSettings authenticationSettings, ILogger<UserService> logger)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _authenticationSettings = authenticationSettings;
        _logger = logger;
    }

    public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

    public int? UserId => User == null ? null : int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

    public async Task<UserDTO> RegisterUserAsync(RegisterUserDTO registerUserDTO)
    {
        // Hash password.
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password);

        // Create UserEntity type with current registered User data.
        var user = new UserEntity();

        user.Username = registerUserDTO.Username;
        user.Email = registerUserDTO.Email;
        user.Role = registerUserDTO.Role;
        user.PasswordHash = passwordHash;

        try
        {
            // Add it...
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync(); // Save the count of entries written in the db.

            // Log the information, that the User has been registered successfully.
            _logger.LogInformation($"User: {registerUserDTO.Username}, Email: {registerUserDTO.Email}, Role: {registerUserDTO.Role}" +
           $"- Has been registered successfully.");

            // If the User is added to the DB return a DTO with his data.
            var userEntity = await _unitOfWork.UserRepository.GetAsync(u => u.Email == user.Email);
            return UserMapper.ToUserDTO(userEntity);
        }
        catch (Exception ex) 
        {
            // Log the information about the exception.
            _logger.LogWarning($"User: {registerUserDTO.Username}, Email: {registerUserDTO.Email}, Role: {registerUserDTO.Role} failed to register. " +
                $"Error Message: {ex.Message}");
        }

        return null;
    }

    public string GenerateToken(UserWithHashDTO userWithHashDTO)
    {
        // List of Claims.
        List<Claim> claims = new()
        {
                new Claim(ClaimTypes.NameIdentifier, userWithHashDTO.Id.ToString()),
                new Claim(ClaimTypes.Name, userWithHashDTO.Username),
                new Claim(ClaimTypes.Email, userWithHashDTO.Email),
                new Claim(ClaimTypes.Role, userWithHashDTO.Role)
            };

        // Create Key.
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JWTKey));

        // Signing Credentials.
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        // Generate Token.
        var token = new JwtSecurityToken(
                _authenticationSettings.JWTIssuer,
                _authenticationSettings.JWTIssuer,
                claims: claims,
                expires: DateTime.Now.AddDays(_authenticationSettings.JWTExpireDays),
                signingCredentials: creds
            );

        // Write the Token.
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        // Return it.
        return jwt;
    }

    public async Task<UserDTO> GetUserByAsync(Expression<Func<UserEntity, bool>> filterExpression, string includeProperties = null)
    {
        // Get UserEntity with fulfill given requirements.
        var userEntity = await _unitOfWork.UserRepository.GetAsync(filterExpression, includeProperties);

        // If no such User exists, return null.
        if (userEntity == null)
        {
            return null;
        }

        // Map it to DTO and return.
        return MapToUserDTO(userEntity);
    }

    public async Task<UserWithHashDTO> GetUserWithHashByAsync(Expression<Func<UserEntity, bool>> filterExpression)
    {
        // Get UserEntity with fulfill given requirements.
        var userEntity = await _unitOfWork.UserRepository.GetAsync(filterExpression);

        // If no such User exists, return null.
        if (userEntity == null)
        {
            return null;
        }

        // Map it to DTO and return.
        return UserMapper.ToUserWithHashDTO(userEntity);
    }

    public async Task<UserDTO> ChangePasswordAsync(ChangeUserPasswordDTO changeUserPasswordDTO)
    {
         // Get UserEntity with fulfill given requirements.
        var userEntity = await _unitOfWork.UserRepository.GetAsync(u => u.Id == changeUserPasswordDTO.Id);

        // If no such User exists, return null.
        if (userEntity == null)
        {
            return null;
        }

        // Hash new password.
        string passwordHash = BCrypt.Net.BCrypt
                .ValidateAndReplacePassword(changeUserPasswordDTO.CurrentPassword, userEntity.PasswordHash, changeUserPasswordDTO.NewPassword);

        // Set the new password hash.
        userEntity.PasswordHash = passwordHash;

        try
        {
            // Update the entity in DB.
            await _unitOfWork.UserRepository.UpdateAsync(userEntity);
            await _unitOfWork.SaveAsync();

            // Log the information, that the User has changed the password successfully.
            _logger.LogInformation($"Id: {userEntity.Id}, User: {userEntity.Username}, Email: {userEntity.Email}" 
                + $"- Has changed password successfully.");

            // Return the User as DTO.
            return UserMapper.ToUserDTO(userEntity);
        }
        catch(Exception ex) 
        {
            // Log the information about the exception.
            _logger.LogWarning($"Id: {userEntity.Id}, User: {userEntity.Username}, Email: {userEntity.Email}" 
                + $"- Has failed to change password. Error Message: {ex.Message}");
        }

        return null;
    }

    public async Task DeleteUserAsync(int answerId)
    {
        // Get UserEntity that should be deleted.
        var userEntity = await _unitOfWork.UserRepository.GetAsync(c => c.Id == answerId);

        try
        {
            // Delete it...
            await _unitOfWork.UserRepository.Remove(userEntity);
            await _unitOfWork.SaveAsync();

            // Log the information, that the User has deleted the account successfully.
            _logger.LogInformation($"Id: {userEntity.Id}, User: {userEntity.Username}, Email: {userEntity.Email}"
             + $"- Has been deleted successfully.");
        }
        catch(Exception ex)
        {
            // Log the information about the exception.
            _logger.LogWarning($"Id: {userEntity.Id}, User: {userEntity.Username}, Email: {userEntity.Email}"
              + $"- Has failed to delete the Account. Error Message: {ex.Message}");
        }
    }

    /// <summary>
    /// Helper responsible for the correct course of mapping process between UserEntity and UserDTO.
    /// Checks if entities have some related data. If yes - map them too.
    /// Saves time because You don't have to do all the work manually every time You want to map Entity to DTO.
    /// </summary>
    /// <param name="userEntity">UserEntity element that will be mapped to DTO.</param>
    /// <returns>UserDTO element with data from given UserEntity as parameter.</returns>
    private static UserDTO MapToUserDTO(UserEntity userEntity)
    {
        var userDTO = UserMapper.ToUserDTO(userEntity);

        // Check if loaded User Entity has related data - Collection of Answer type.
        if (userEntity.Answers != null && userEntity.Answers.Count != 0)
        {
            // Set Answers collection...
            userDTO.Answers = userEntity.Answers.Select(AnswerMapper.ToAnswerDTO).ToList();
        }

        // Check if loaded User Entity has related data - Collection of Rating type.
        if (userEntity.Ratings != null && userEntity.Ratings.Count != 0)
        {
            // Set Ratings collection...
            userDTO.Ratings = userEntity.Ratings.Select(RatingMapper.ToRatingDTO).ToList();
        }

        // Check if loaded User Entity has related data - Collection of Question type.
        if (userEntity.Questions != null && userEntity.Questions.Count != 0)
        {
            // Set Questions collection...
            userDTO.Questions = userEntity.Questions.Select(QuestionMapper.ToQuestionDTO).ToList();
        }

        return userDTO;
    }
}
