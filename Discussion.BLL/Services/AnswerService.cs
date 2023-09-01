using Discussion.BLL.Services.Interfaces;
using Discussion.DAL.Repository.UnitOfWork;
using Discussion.Entities;
using Discussion.Models.DTO_s.AnswerDTO_s;
using Discussion.Models.DTO_s.QuestionDTO_s;
using Discussion.Models.DTO_s.RatingDTO_s;
using Discussion.Models.DTO_s.UserDTO_s;
using System.Linq.Expressions;

namespace Discussion.BLL.Services;

internal class AnswerService : IAnswerService
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

        // Map from AnswerEntity to AnswerDTO collection.
        var answerDTOList = new List<AnswerDTO>();

        foreach (var answerEntity in answerEntityCollection)
        {
            var answerDTO = MapToAnswerDTO(answerEntity);

            answerDTOList.Add(answerDTO);
        }

        // Return Collection of answerDTO type.
        return answerDTOList;

    }

    public async Task<AnswerDTO> GetAnswerByAsync(Expression<Func<AnswerEntity, bool>> filterExpression, string includeProperties = null)
    {
        // Get AnswerEntity with fulfill given requirements.
        var answerEntity = await _unitOfWork.AnswerRepository.GetAsync(filterExpression, includeProperties);

        // Map it to DTO.
        var answerDTO = MapToAnswerDTO(answerEntity);

        // Return answerDTO with mapped AnswerEntity data.
        return answerDTO;
    } 

    public async Task<AnswerDTO> InsertAnswerAsync(CreateAnswerDTO createAnswerDTO)
    {
        // Create new AnswerEntity with given data.
        var answerEntity = new AnswerEntity
        {
            Content = createAnswerDTO.Content,
            QuestionId = createAnswerDTO.QuestionId,
            UserId = createAnswerDTO.UserId
        };

        // Add it...
        await _unitOfWork.AnswerRepository.AddAsync(answerEntity);
        await _unitOfWork.SaveAsync();

        // Map it to DTO.
        var answerDTO = MapToAnswerDTO(answerEntity);

        // Return current inserted Entity mapped to DTO.
        return answerDTO;
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
        var answerDTO = MapToAnswerDTO(answerEntity);

        // Return current mapped Entity as DTO.
        return answerDTO;
    }

    public async Task DeleteAnswerAsync(int answerId)
    {
        // Get AnswerEntity that should be deleted.
        var answerEntity = await _unitOfWork.AnswerRepository.GetAsync(c => c.Id == answerId);

        // Delete it...
        await _unitOfWork.AnswerRepository.Remove(answerEntity);
        await _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Helper responsible for the correct course of mapping process between AnswerEntity and AnswerDTO.
    /// Checks if entities have some related data. If yes - map them too.
    /// Saves code because You don't have to do all the work manually every time You want to map Entity to DTO.
    /// </summary>
    /// <param name="answerEntity">AnswerEntity element that will be mapped to DTO.</param>
    /// <returns>AnswerDTO element with data from given AnswerEntity as parameter.</returns>
    private AnswerDTO MapToAnswerDTO(AnswerEntity answerEntity)
    {
        var answerDTO = AnswerDTO.ToAnswerDTO(answerEntity);

        // Check if loaded Answer Entity has related data - Question.
        if (answerEntity.Question != null)
        {
            // If yes - map the QuestionEntity to DTO and add to the QuestionDTO property located in AnswerDTO.
            answerDTO.Question = QuestionDTO.ToQuestionDTO(answerEntity.Question);
        }

        // Check if loaded Answer Entity has related data - User.
        if (answerDTO.User != null)
        {
            // If yes - map the UserEntity to DTO and add to the UserDTO property located in AnswerDTO.
            answerDTO.User = UserDTO.ToUserDTO(answerEntity.User);
        }

        // Check if loaded AnswerEntity has related data - Collection of Rating type.
        if (answerEntity.Ratings != null && answerEntity.Ratings.Count() != 0)
        {
            // If yes - map each to DTO and add to the RatingDTO collection located in AnswerDTO.
            foreach (var ratingEntity in answerEntity.Ratings)
            {
                answerDTO.Ratings.Add(RatingDTO.ToRatingDTO(ratingEntity));
            }
        }

        return answerDTO;
    }

}
