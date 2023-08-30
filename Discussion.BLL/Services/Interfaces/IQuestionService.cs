using Discussion.Entities;
using Discussion.Models.DTO_s.QuestionDTO_s;
using System.Linq.Expressions;

namespace Discussion.BLL.Services.Interfaces;

public interface IQuestionService
{
    /// <summary>
    /// Get all Questions that fulfill given filterExpression if it is given. If not Get all Questions.
    /// </summary>
    /// <param name="filterExpression">Optional requirement's that must be fulfilled if given.</param>
    /// <param name="includeProperties">Optional related properties.</param>
    /// <returns>Collection of QuestionDTO type.</returns>
    Task<IEnumerable<QuestionDTO>> GetQuestionsAsync(Expression<Func<QuestionEntity, bool>> filterExpression = null, string includeProperties = null);

    /// <summary>
    /// Get a specific Question that fulfill given filterExpression.
    /// </summary>
    /// <param name="filterExpression">Requirement's that must be fulfilled by a Question to be returned.</param>
    /// <param name="includeProperties">Optional related properties.</param>
    /// <returns>QuestionDTO type.</returns>
    Task<QuestionDTO> GetQuestionByAsync(Expression<Func<QuestionEntity, bool>> filterExpression, string includeProperties = null);

    /// <summary>
    /// Insert new Question type to the DB.
    /// </summary>
    /// <param name="createQuestionDTODTO">DTO that contains data for a Question that should be added to the DB.</param>
    /// <returns>QuestionDTO type with the just created Question data.</returns>
    Task<QuestionDTO> InsertQuestionAsync(CreateQuestionDTO createQuestionDTODTO);

    /// <summary>
    /// Update given Question in the DB.
    /// </summary>
    /// <param name="updateQuestionDTO">DTO that contains data for a Question that should be updated in the DB.</param>
    /// <returns>QuestionDTO type with the just updated Question data.</returns>
    Task<QuestionDTO> UpdateQuestionAsync(QuestionDTO updateQuestionDTO);

    /// <summary>
    /// Delete given Question from the DB.
    /// </summary>
    /// <param name="questionId">Id of Question that should be deleted.</param>
    Task DeleteQuestionAsync(int questionId);
}
