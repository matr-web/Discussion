using Discussion.DAL.Repository.IRepository;
using Discussion.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discussion.DAL.Repository;

internal class AnswerRepository : Repository<AnswerEntity>, IAnswerRepository
{
    private readonly DiscussDbContext _db;
    // 'DiscussDbContext' element is required in 'Repository' class.
    internal AnswerRepository(DiscussDbContext db) : base(db)
    {
        _db = db;
    }

    // Implement here the method's from 'IAnswerRepository'... if You add some.
}
