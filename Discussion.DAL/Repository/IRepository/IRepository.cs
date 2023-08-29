using System.Linq.Expressions;

namespace Discussion.DAL.Repository.IRepository;

/// <summary>
/// Repository Pattern.
/// Generic Repository Interface which supplies method's for Db operation's on all entities. 
/// </summary>
/// <typeparam name="T">Type T, which will become the currently used Entity Type on which the given database operations will be performed.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Get all element's of Type T or those that match given predicate.
    /// </summary>
    /// <param name="predicate">Optional requirements which given element's of Type T have to fulfill to be returned.</param>
    /// <param name="includeProperties">Optional Include Properties that if given will be loaded together with each Type T element.</param>
    /// <returns>Collection of Type T element's.</returns>
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, string includeProperties = null);

    /// <summary>
    /// Get first or default Type T element which fulfills given predicate.
    /// </summary>
    /// <param name="predicate">Requirements which given element of Type T has to fulfill to be returned.</param>
    /// <param name="includeProperties">Optional Include Properties that if given will be loaded together with returned Type T element.</param>
    /// <returns>First or default Type T element.</returns>
    Task<T> GetAsync(Expression<Func<T, bool>> predicate, string includeProperties = null);

    /// <summary>
    /// Add new Type T element into the Db.
    /// </summary>
    /// <param name="entity">Element that will be added into the Db.</param>
    /// <returns>The successfully completed Task.</returns>
    Task AddAsync(T entity);

    /// <summary>
    /// Update given Type T element in the Db.
    /// </summary>
    /// <param name="entity">Element that will be updated in the Db.</param>
    /// <returns>The successfully completed Task.</returns>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Remove given Type T element from the Db.
    /// </summary>
    /// <param name="entity">Element that will be deleted from the Db.</param>
    /// <returns>The successfully completed Task.</returns>
    Task Remove(T entity);

    /// <summary>
    /// Remove range of Type T element's from the Db.
    /// </summary>
    /// <param name="entities">Collection of Type T element's that will be deleted from the Db.</param>
    /// <returns>The successfully completed Task.</returns>
    Task RemoveRange(IEnumerable<T> entities);
}
