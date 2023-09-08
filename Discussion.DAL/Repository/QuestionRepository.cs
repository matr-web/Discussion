using Discussion.DAL.Repository.IRepository;
using Discussion.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discussion.DAL.Repository;

internal class QuestionRepository : Repository<QuestionEntity>, IQuestionRepository
{
    private readonly DiscussDbContext _db;
    internal QuestionRepository(DiscussDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task Remove(QuestionEntity questionEntity)
    {
        QuestionEntity question = await _db.Questions
            .Include(q => q.Answers)
            .ThenInclude(a => a.Ratings)
            .Include(q => q.Ratings)
            .FirstOrDefaultAsync(q => q.Id == questionEntity.Id);

        _db.Questions.Remove(question);
    }
}