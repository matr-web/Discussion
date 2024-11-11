using Discussion.BLL.Services.Interfaces;
using Discussion.DAL.Repository.UnitOfWork;
using Discussion.Entities;
using Discussion.Models.DTO_s.AnswerDTO_s;
using Discussion.Models.DTO_s.QuestionDTO_s;
using Discussion.Models.DTO_s.RatingDTO_s;
using Discussion.Models.DTO_s.UserDTO_s;
using Discussion.Utility.Mappers;
using System.Linq.Expressions;

namespace Discussion.BLL.Services;

public class QuestionService : IQuestionService
{
    private readonly IUnitOfWork _unitOfWork;

    public QuestionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    } 

    public async Task<IEnumerable<QuestionDTO>> GetQuestionsAsync(string orderByProperty, Expression<Func<QuestionEntity, bool>> filterExpression = null, string includeProperties = null)
    {
        // Get all needed Question Entities with fulfill given requirements.
        var questionEntityCollection = await _unitOfWork.QuestionRepository.GetAllAsync(filterExpression, includeProperties);

        // If there are no Question's, return null.
        if (questionEntityCollection.Count() == 0)
        {
            return null;
        }

        // Map from QuestionEntity to QuestionDTO collection.
        var questionDTOList = new List<QuestionDTO>();

        foreach (var questionEntity in questionEntityCollection)
        {
            var questionDTO = MapToQuestionDTO(questionEntity);

            questionDTOList.Add(questionDTO);
        }

        // Return collection of QuestionDTO ordered by the given property.
        return questionDTOList.OrderBy(q => q.GetType().GetProperty(orderByProperty).GetValue(q));
    }

    public async Task<QuestionDTO> GetQuestionByAsync(Expression<Func<QuestionEntity, bool>> filterExpression, string includeProperties = null)
    {
        // Get QuestionEntity with fulfill given requirements.
        var questionEntity = await _unitOfWork.QuestionRepository.GetAsync(filterExpression, includeProperties);

        // If no such Question exists, return null.
        if (questionEntity == null)
        {
            return null;
        }

        // Map it to DTO.
        var questionDTO = MapToQuestionDTO(questionEntity);

        // Return QuestionDTO with mapped QuestionEntity data.
        return questionDTO;
    }

    public async Task<QuestionDTO> InsertQuestionAsync(CreateQuestionDTO createQuestionDTODTO, int userId)
    {
        // Create new QuestionEntity with given data.
        var questionEntity = new QuestionEntity
        {
            CategoryId = createQuestionDTODTO.CategoryId,
            Topic = createQuestionDTODTO.Topic,
            Content = createQuestionDTODTO.Content,
            UserId = userId
        };

        // Add it...
        await _unitOfWork.QuestionRepository.AddAsync(questionEntity);
        await _unitOfWork.SaveAsync();

        // Map it to DTO.
        var questionDTO = MapToQuestionDTO(questionEntity);

        // Return current mapped DTO.
        return questionDTO;
    }

    public async Task<QuestionDTO> UpdateQuestionAsync(UpdateQuestionDTO updateQuestionDTO)
    {
        // Get QuestionEntity that will be updated.
        var questionEntity = await _unitOfWork.QuestionRepository.GetAsync(c => c.Id == updateQuestionDTO.Id, "Category,User,Answers,Ratings");

        // Change the value's of updated properties.
        questionEntity.CategoryId = updateQuestionDTO.CategoryId;
        questionEntity.Topic = updateQuestionDTO.Topic;
        questionEntity.Content = updateQuestionDTO.Content;

        // Update it...
        await _unitOfWork.QuestionRepository.UpdateAsync(questionEntity);
        await _unitOfWork.SaveAsync();

        // Map it to DTO.
        var questionDTO = MapToQuestionDTO(questionEntity);

        // Return current inserted Entity mapped to DTO.
        return questionDTO;
    }

    public async Task DeleteQuestionAsync(int questionId)
    {
        // Get QuestionEntity that should be deleted.
        var questionEntity = await _unitOfWork.QuestionRepository.GetAsync(c => c.Id == questionId);

        // Delete it...
        await _unitOfWork.QuestionRepository.Remove(questionEntity);
        await _unitOfWork.SaveAsync();
    }

    public PaginatedQuestionDTOs PaginateQuestions(IEnumerable<QuestionDTO> questionDTOs, int currentPage, int pageSize)
    {
        // Get the count of all questions.
        var questionCount = questionDTOs.Count();

        // Calculate the total pages count.
        var totalPages = (int)Math.Ceiling((decimal)questionCount / pageSize);

        // Get the element's on given page.
        questionDTOs = questionDTOs
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var paginatedQuestionDTOs = QuestionMapper.ToPaginatedQuestionsDTO(questionDTOs, currentPage, totalPages);

        return paginatedQuestionDTOs;
    }

    /// <summary>
    /// Helper responsible for the correct course of mapping process between QuestionEntity and QuestionDTO.
    /// Checks if entities have some related data. If yes - map them too.
    /// Saves time because You don't have to do all the work manually every time You want to map Entity to DTO.
    /// </summary>
    /// <param name="questionEntity">QuestionEntity element that will be mapped to DTO.</param>
    /// <returns>QuestionDTO element with data from given QuestionEntity as parameter.</returns>
    private static QuestionDTO MapToQuestionDTO(QuestionEntity questionEntity)
    {
        var questionDTO = QuestionMapper.ToQuestionDTO(questionEntity);

        // Check if loaded Question Entity has related data - Category.
        if (questionEntity.Category != null)
        {
            // If yes - map the CategoryEntity to DTO and add to the CategoryDTO property located in QuestionDTO.
            questionDTO.Category = CategoryMapper.ToCategoryDTO(questionEntity.Category);
        }

        // Check if loaded Question Entity has related data - Author.
        if (questionEntity.User != null)
        {
            // If yes - map the UserEntity to DTO and add to the UserDTO property located in QuestionDTO.
            questionDTO.User = UserMapper.ToUserDTO(questionEntity.User);
        }

        // Check if loaded Question Entity has related data - Collection of Answer type.
        if (questionEntity.Answers != null && questionEntity.Answers.Count() != 0)
        {
            // Create blanc AnswerDto type list.
            var answersList = new List<AnswerDTO>();

            // If yes - map each to DTO and add to the AnswerDTO collection located in QuestionDTO.
            foreach (var answerEntity in questionEntity.Answers)
            {
                answersList.Add(AnswerMapper.ToAnswerDTO(answerEntity));
            }

            // Set Answers collection...
            questionDTO.Answers = answersList;
        }

        // Check if loaded Question Entity has related data - Collection of Answer type.
        if (questionEntity.Ratings != null && questionEntity.Ratings.Count() != 0)
        {
            // Create blanc RatingDto type list.
            var ratingsList = new List<RatingDTO>();

            // If yes - map each to DTO and add to the RatingDTO collection located in QuestionDTO.
            foreach (var ratingEntity in questionEntity.Ratings)
            {

                ratingsList.Add(RatingMapper.ToRatingDTO(ratingEntity));
            }

            // Set Ratings collection...
            questionDTO.Ratings = ratingsList;
        }

        return questionDTO;
    }
}
