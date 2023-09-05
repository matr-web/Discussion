using Discussion.DAL.Repository.IRepository;

namespace Discussion.DAL.Repository.UnitOfWork;

/// <summary>
/// Interface that contains all Repositories and Global Method's for Repositories.
/// Thank's to that You have access to all Repositories and Global Method's through just one IUnitOfWork object.
/// </summary>
public interface IUnitOfWork
{
    // Repositories.
    IAnswerRepository AnswerRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IQuestionRepository QuestionRepository { get; }
    IRatingRepository RatingRepository { get; }
    IUserRepository UserRepository { get; }


    // Global Method's.
    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <returns> A task that represents the asynchronous save operation. 
    /// The task result contains the number of state entries written to the database.</returns>
    Task<int> SaveAsync();
}
