using Discussion.BLL.Services.Interfaces;
using Discussion.DAL.Repository.UnitOfWork;
using Discussion.Entities;
using Discussion.Models.DTO_s.QuestionDTO_s;
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
        if (!questionEntityCollection.Any())
        {
            return null;
        }

        // Else map them to dto's and return ordered by the given property.
        return questionEntityCollection.Select(MapToQuestionDTO).OrderBy(q => q.GetType().GetProperty(orderByProperty).GetValue(q));
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

        // Map it to DTO and return.
        return MapToQuestionDTO(questionEntity);
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

        // Map it to DTO and return.
        return MapToQuestionDTO(questionEntity);
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

        // Map it to DTO and return.
        return MapToQuestionDTO(questionEntity);
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
        if (questionEntity.Answers != null && questionEntity.Answers.Count != 0)
        {
            // Set Answers collection...
            questionDTO.Answers = questionEntity.Answers.Select(AnswerMapper.ToAnswerDTO).ToList();
        }

        // Check if loaded Question Entity has related data - Collection of Answer type.
        if (questionEntity.Ratings != null && questionEntity.Ratings.Count != 0)
        {
            // Set Ratings collection...
            questionDTO.Ratings = questionEntity.Ratings.Select(RatingMapper.ToRatingDTO).ToList();
        }

        return questionDTO;
    }
}
