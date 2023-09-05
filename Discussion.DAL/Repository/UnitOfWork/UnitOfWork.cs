using Discussion.DAL.Repository.IRepository;

namespace Discussion.DAL.Repository.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DiscussDbContext _db;

    public UnitOfWork(DiscussDbContext db)
    {
        _db = db;

        AnswerRepository = new AnswerRepository(db);
        CategoryRepository = new CategoryRepository(db);
        QuestionRepository = new QuestionRepository(db);
        RatingRepository = new RatingRepository(db);
        UserRepository = new UserRepository(db);
    }

    public IAnswerRepository AnswerRepository { get; private set; }
    public ICategoryRepository CategoryRepository { get; private set; }
    public IQuestionRepository QuestionRepository { get; private set; }
    public IRatingRepository RatingRepository { get; private set; }
    public IUserRepository UserRepository { get; private set; }

    // Implementation of Global Method's for all repositories.
    public async Task<int> SaveAsync()
    {
       return await _db.SaveChangesAsync();
    }
}
