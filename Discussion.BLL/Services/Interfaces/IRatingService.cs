using Discussion.Entities;
using Discussion.Models.DTO_s.CategoryDTO_s;
using Discussion.Models.DTO_s.RatingDTO_s;
using System.Linq.Expressions;

namespace Discussion.BLL.Services.Interfaces;

public interface IRatingService
{
    /// <summary>
    /// Get all Ratings that fulfill given filterExpression if it is given.
    /// </summary>
    /// <param name="filterExpression">Requirement's that must be fulfilled if given.</param>
    /// <param name="includeProperties">Optional related properties.</param>
    /// <returns>Collection of RatingDTO type.</returns>
    Task<IEnumerable<RatingDTO>> GetRatingsAsync(Expression<Func<RatingEntity, bool>> filterExpression = null, string includeProperties = null);

    /// <summary>
    /// Get a specific Rating that fulfill given filterExpression.
    /// </summary>
    /// <param name="filterExpression">Requirement's that must be fulfilled by a Rating to be returned.</param>
    /// <param name="includeProperties">Optional related properties.</param>
    /// <returns>RatingDTO type.</returns>
    Task<RatingDTO> GetRatingByAsync(Expression<Func<RatingEntity, bool>> filterExpression, string includeProperties = null);

    /// <summary>
    /// Insert new Rating type to the DB.
    /// </summary>
    /// <param name="createRatingDTO">DTO that contains data for a Rating that should be added to the DB.</param>
    /// <param name="userId">User Id that create's given Answer.</param>
    /// <returns>RatingDTO type with the just created Rating data.</returns>
    Task<RatingDTO> InsertRatingAsync(CreateRatingDTO createRatingDTO, int userId);

    /// <summary>
    /// Delete given Rating from the DB.
    /// </summary>
    /// <param name="ratingId">Id of Rating that should be deleted.</param>
    Task DeleteRatingAsync(int ratingId);
}
