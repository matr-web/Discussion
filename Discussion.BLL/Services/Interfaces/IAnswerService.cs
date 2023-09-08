using Discussion.Entities;
using Discussion.Models.DTO_s.AnswerDTO_s;
using System.Linq.Expressions;

namespace Discussion.BLL.Services.Interfaces;

public interface IAnswerService
{
    /// <summary>
    /// Get all Answers that fulfill given filterExpression if it is given. If not Get all Answers.
    /// </summary>
    /// <param name="filterExpression">Optional requirement's that must be fulfilled if given.</param>
    /// <param name="includeProperties">Optional related properties.</param>
    /// <returns>Collection of AnswerDTO type.</returns>
    Task<IEnumerable<AnswerDTO>> GetAnswersAsync(Expression<Func<AnswerEntity, bool>> filterExpression = null, string includeProperties = null);

    /// <summary>
    /// Get a specific Answer that fulfill given filterExpression.
    /// </summary>
    /// <param name="filterExpression">Requirement's that must be fulfilled by a Answer to be returned.</param>
    /// <param name="includeProperties">Optional related properties.</param>
    /// <returns>AnswerDTO type.</returns>
    Task<AnswerDTO> GetAnswerByAsync(Expression<Func<AnswerEntity, bool>> filterExpression, string includeProperties = null);

    /// <summary>
    /// Insert new Answer type to the DB.
    /// </summary>
    /// <param name="createAnswerDTO">DTO that contains data for a Answer that should be added to the DB.</param>
    /// <param name="userId">User Id that create's given Answer.</param>
    /// <returns>AnswerDTO type with the just created Answer data.</returns>
    Task<AnswerDTO> InsertAnswerAsync(CreateAnswerDTO createAnswerDTO, int userId);

    /// <summary>
    /// Update given Answer in the DB.
    /// </summary>
    /// <param name="updateAnswerDTO">DTO that contains data for a Answer that should be updated in the DB.</param>
    /// <returns>AnswerDTO type with the just updated Answer data.</returns>
    Task<AnswerDTO> UpdateAnswerAsync(UpdateAnswerDTO updateAnswerDTO);

    /// <summary>
    /// Delete given Answer from the DB.
    /// </summary>
    /// <param name="answerId">Id of Answer that should be deleted.</param>
    Task DeleteAnswerAsync(int answerId);
}
