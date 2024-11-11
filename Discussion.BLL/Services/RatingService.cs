using Discussion.BLL.Services.Interfaces;
using Discussion.Entities;
using Discussion.Models.DTO_s.RatingDTO_s;
using System.Linq.Expressions;
using Discussion.DAL.Repository.UnitOfWork;
using Discussion.Utility.Mappers;

namespace Discussion.BLL.Services;

public class RatingService : IRatingService
{
    private readonly IUnitOfWork _unitOfWork;

    public RatingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<RatingDTO>> GetRatingsAsync(Expression<Func<RatingEntity, bool>> filterExpression = null, string includeProperties = null)
    {
        // Get all needed Rating Entities with fulfill given requirements.
        var ratingEntityCollection = await _unitOfWork.RatingRepository.GetAllAsync(filterExpression, includeProperties);

        // If there are no Rating's, return null.
        if(!ratingEntityCollection.Any())
        {
            return null;
        }

        // Else map it to dto's and return it.
        return ratingEntityCollection.Select(RatingMapper.ToRatingDTO);
    }

    public async Task<RatingDTO> GetRatingByAsync(Expression<Func<RatingEntity, bool>> filterExpression, string includeProperties = null)
    {
        // Get RatingEntity with fulfill given requirements.
        var ratingEntity = await _unitOfWork.RatingRepository.GetAsync(filterExpression, includeProperties);

        // If no such Rating exists, return null.
        if (ratingEntity == null)
        {
            return null;
        }

        // Map it to DTO and return.
        return MapToRatingDTO(ratingEntity);
    }

    public async Task<RatingDTO> InsertRatingAsync(CreateRatingDTO createRatingDTO, int userId)
    {
        // Create new RatingEntity with given data.
        var ratingEntity = new RatingEntity
        {
            Value = createRatingDTO.Value,
            UserId = userId,
            QuestionId = createRatingDTO.QuestionId != 0 ? createRatingDTO.QuestionId : null,
            AnswerId = createRatingDTO.AnswerId != 0 ? createRatingDTO.AnswerId : null,
        };

        // Add it...
        await _unitOfWork.RatingRepository.AddAsync(ratingEntity);
        await _unitOfWork.SaveAsync();

        // Map it to DTO and return.
        return MapToRatingDTO(ratingEntity);
    }

    public async Task DeleteRatingAsync(int ratingId)
    {
        // Get RatingEntity that should be deleted.
        var ratingEntity = await _unitOfWork.RatingRepository.GetAsync(c => c.Id == ratingId);

        // Delete it...
        await _unitOfWork.RatingRepository.Remove(ratingEntity);
        await _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Helper responsible for the correct course of mapping process between RatingEntity and RatingDTO.
    /// Checks if entities have some related data. If yes - map them too.
    /// Saves time because You don't have to do all the work manually every time You want to map Entity to DTO.
    /// </summary>
    /// <param name="ratingEntity">RatingEntity element that will be mapped to DTO.</param>
    /// <returns>RatingDTO element with data from given RatingEntity as parameter.</returns>
    private static RatingDTO MapToRatingDTO(RatingEntity ratingEntity)
    {
        var ratingDTO = RatingMapper.ToRatingDTO(ratingEntity);

        // Check if loaded Rating Entity has related data - User.
        if (ratingDTO.User != null)
        {
            // If yes - map the UserEntity to DTO and add to the UserDTO property located in RatingDTO.
            ratingDTO.User = UserMapper.ToUserDTO(ratingEntity.User);
        }

        // Check if loaded Rating Entity has related data - Question.
        if (ratingEntity.Question != null)
        {
            // If yes - map the QuestionEntity to DTO and add to the QuestionDTO property located in RatingDTO.
            ratingDTO.Question = QuestionMapper.ToQuestionDTO(ratingEntity.Question);
        }

        // Check if loaded Rating Entity has related data - Answer.
        if (ratingDTO.Answer != null)
        {
            // If yes - map the AnswerEntity to DTO and add to the AnswerDTO property located in RatingDTO.
            ratingDTO.Answer = AnswerMapper.ToAnswerDTO(ratingEntity.Answer);
        }

        return ratingDTO;
    }
}
