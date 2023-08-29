using Discussion.DAL.Repository.IRepository;
using Discussion.Entities;

namespace Discussion.DAL.Repository;

internal class QuestionRepository : Repository<QuestionEntity>, IQuestionRepository
{
    internal QuestionRepository(DiscussDbContext db) : base(db)
    {

    }
}