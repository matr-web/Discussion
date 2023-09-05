using Azure.Core;
using Discussion.BLL.Services.Interfaces;
using Discussion.DAL.Repository.UnitOfWork;
using Discussion.Entities;
using Discussion.Models.DTO_s.UserDTO_s;
using Discussion.Models.DTO_s.QuestionDTO_s;
using System.Linq.Expressions;
using Discussion.Models.DTO_s.AnswerDTO_s;
using Discussion.Models.DTO_s.RatingDTO_s;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Discussion.BLL.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    //private readonly AuthenticationSettings _authenticationSettings;

    public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork; 
        _configuration = configuration; 
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
        
        // Map it to DTO.
        var userDTO = MapToUserDTO(userEntity);

        // Return userDTO with mapped UserEntity data.
        return userDTO;
    }

    public async Task<UserDTO> RegisterUserAsync(RegisterUserDTO registerUserDto)
    {
        // Hash password.
        string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password);

        // Create UserEntity type with current registered User data.
        var user = new UserEntity();

        user.Username = registerUserDto.Username;
        user.Email = registerUserDto.Email;
        user.Role = registerUserDto.Role;
        user.PasswordHash = passwordHash;

        // Add it...
        await _unitOfWork.UserRepository.AddAsync(user);
        var entriesWrittenCount = await _unitOfWork.SaveAsync();

        // If the User is added to the DB return a DTO with his data.
        if (entriesWrittenCount >= 1)
        {
            var userEntity = await _unitOfWork.UserRepository.GetAsync(u => u.Email == user.Email);
            return UserDTO.ToUserDTO(userEntity);
        }

        // if not return null.
        return null;
    }

    public async Task<string> GenerateToken(UserDTO userDTO)
    {
        // List of Claims.
        List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, userDTO.Username),
                new Claim(ClaimTypes.Email, userDTO.Email),
                new Claim(ClaimTypes.Role, userDTO.Role)
            };

        // Create Key.
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value!));

        // Signing Credentials.
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        // Generate Token.
        var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

        // Write the Token.
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
         
        // Return it.
        return jwt;
    }

    /// <summary>
    /// Helper responsible for the correct course of mapping process between UserEntity and UserDTO.
    /// Checks if entities have some related data. If yes - map them too.
    /// Saves time because You don't have to do all the work manually every time You want to map Entity to DTO.
    /// </summary>
    /// <param name="userEntity">UserEntity element that will be mapped to DTO.</param>
    /// <returns>UserDTO element with data from given UserEntity as parameter.</returns>
    private UserDTO MapToUserDTO(UserEntity userEntity)
    {
        var userDTO = UserDTO.ToUserDTO(userEntity);

        // Check if loaded User Entity has related data - Collection of Answer type.
        if (userEntity.Answers != null && userEntity.Answers.Count() != 0)
        {
            // If yes - map each to DTO and add to the AnswerDTO collection located in UserDTO.
            foreach (var answerEntity in userEntity.Answers)
            {
                userDTO.Answers.Add(AnswerDTO.ToAnswerDTO(answerEntity));
            }
        }

        // Check if loaded User Entity has related data - Collection of Rating type.
        if (userEntity.Ratings != null && userEntity.Ratings.Count() != 0)
        {
            // If yes - map each to DTO and add to the RatingDTO collection located in UserDTO.
            foreach (var ratingEntity in userEntity.Ratings)
            {
                userDTO.Ratings.Add(RatingDTO.ToRatingDTO(ratingEntity));
            }
        }

        // Check if loaded User Entity has related data - Collection of Question type.
        if (userEntity.Questions != null && userEntity.Questions.Count() != 0)
        {
            // If yes - map each to DTO and add to the QuestionDTO collection located in UserDTO.
            foreach (var questionEntity in userEntity.Questions)
            {
                userDTO.Questions.Add(QuestionDTO.ToQuestionDTO(questionEntity));
            }
        }

        return userDTO;
    }
}
