using Discussion.DAL.Repository.IRepository;
using Discussion.Entities;

namespace Discussion.DAL.Repository;

internal class AnswerRepository : Repository<AnswerEntity>, IAnswerRepository
{
    // 'DiscussDbContext' element is required in 'Repository' class.
    internal AnswerRepository(DiscussDbContext db) : base(db)
    {

    }

    // Implement here the method's from 'IAnswerRepository'... if You add some.
}
