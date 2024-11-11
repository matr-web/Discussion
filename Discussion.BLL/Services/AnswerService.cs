using Discussion.BLL.Services.Interfaces;
using Discussion.DAL.Repository.UnitOfWork;
using Discussion.Entities;
using Discussion.Models.DTO_s.AnswerDTO_s;
using Discussion.Utility.Mappers;
using System.Linq.Expressions;

namespace Discussion.BLL.Services;

public class AnswerService : IAnswerService
{
    private readonly IUnitOfWork _unitOfWork;

    public AnswerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }  

    public async Task<IEnumerable<AnswerDTO>> GetAnswersAsync(Expression<Func<AnswerEntity, bool>> filterExpression = null, string includeProperties = null)
    {
        // Get all needed Answer Entities with fulfill given requirements.
        var answerEntityCollection = await _unitOfWork.AnswerRepository.GetAllAsync(filterExpression, includeProperties);

        // If there are no Answer's, return null.
        if (!answerEntityCollection.Any())
        {
            return null;
        }

        // Else map them to dto's and return ordered by the Name property.
        return answerEntityCollection.Select(MapToAnswerDTO).OrderBy(a => a.Date);
    }

    public async Task<AnswerDTO> GetAnswerByAsync(Expression<Func<AnswerEntity, bool>> filterExpression, string includeProperties = null)
    {
        // Get AnswerEntity with fulfill given requirements.
        var answerEntity = await _unitOfWork.AnswerRepository.GetAsync(filterExpression, includeProperties);

        // If no such Answer exists, return null.
        if (answerEntity == null)
        {
            return null;
        }

        // Map it to DTO and return.
        return MapToAnswerDTO(answerEntity);
    } 

    public async Task<AnswerDTO> InsertAnswerAsync(CreateAnswerDTO createAnswerDTO, int userId)
    {
        // Create new AnswerEntity with given data.
        var answerEntity = new AnswerEntity
        {
            Content = createAnswerDTO.Content,
            QuestionId = createAnswerDTO.QuestionId,
            UserId = userId
        };

        // Add it...
        await _unitOfWork.AnswerRepository.AddAsync(answerEntity);
        await _unitOfWork.SaveAsync();

        // Map it to DTO and return.
        return MapToAnswerDTO(answerEntity);
    }

    public async Task<AnswerDTO> UpdateAnswerAsync(UpdateAnswerDTO updateAnswerDTO)
    {
        // Get AnswerEntity that will be updated.
        var answerEntity = await _unitOfWork.AnswerRepository.GetAsync(c => c.Id == updateAnswerDTO.Id, "Question,User");

        // Change the value's of updated properties.
        answerEntity.Content = updateAnswerDTO.Content;

        // Update it...
        await _unitOfWork.AnswerRepository.UpdateAsync(answerEntity);
        await _unitOfWork.SaveAsync();

        // Map it to DTO.
        return MapToAnswerDTO(answerEntity);
    }

    public async Task DeleteAnswerAsync(int answerId)
    {
        // Get AnswerEntity that should be deleted.
        var answerEntity = await _unitOfWork.AnswerRepository.GetAsync(c => c.Id == answerId, "Ratings");

        // Delete it...
        await _unitOfWork.AnswerRepository.Remove(answerEntity);
        await _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Helper responsible for the correct course of mapping process between AnswerEntity and AnswerDTO.
    /// Checks if entities have some related data. If yes - map them too.
    /// Saves time because You don't have to do all the work manually every time You want to map Entity to DTO.
    /// </summary>
    /// <param name="answerEntity">AnswerEntity element that will be mapped to DTO.</param>
    /// <returns>AnswerDTO element with data from given AnswerEntity as parameter.</returns>
    private static AnswerDTO MapToAnswerDTO(AnswerEntity answerEntity)
    {
        var answerDTO = AnswerMapper.ToAnswerDTO(answerEntity);

        // Check if loaded Answer Entity has related data - Question.
        if (answerEntity.Question != null)
        {
            // If yes - map the QuestionEntity to DTO and add to the QuestionDTO property located in AnswerDTO.
            answerDTO.Question = QuestionMapper.ToQuestionDTO(answerEntity.Question);
        }

        // Check if loaded Answer Entity has related data - User.
        if (answerDTO.User != null)
        {
            // If yes - map the UserEntity to DTO and add to the UserDTO property located in AnswerDTO.
            answerDTO.User = UserMapper.ToUserDTO(answerEntity.User);
        }

        // Check if loaded AnswerEntity has related data - Collection of Rating type.
        if (answerEntity.Ratings != null && answerEntity.Ratings.Count != 0)
        {
            // If yes - map each rating to DTO and add to the RatingDTO collection located in AnswerDTO.
            answerDTO.Ratings = answerEntity.Ratings.Select(RatingMapper.ToRatingDTO).ToList();
        }

        return answerDTO;
    }
}
