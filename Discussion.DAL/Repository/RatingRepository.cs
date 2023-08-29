using Discussion.DAL.Repository.IRepository;
using Discussion.Entities;

namespace Discussion.DAL.Repository;

internal class RatingRepository : Repository<RatingEntity>, IRatingRepository
{
    internal RatingRepository(DiscussDbContext db) : base(db)
    {

    }
}