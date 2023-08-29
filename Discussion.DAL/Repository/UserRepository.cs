using Discussion.DAL.Repository.IRepository;
using Discussion.Entities;

namespace Discussion.DAL.Repository;

internal class UserRepository : Repository<UserEntity>, IUserRepository
{
    internal UserRepository(DiscussDbContext db) : base(db)
    {

    }
}

