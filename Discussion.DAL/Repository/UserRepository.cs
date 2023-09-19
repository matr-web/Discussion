using Discussion.DAL.Repository.IRepository;
using Discussion.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discussion.DAL.Repository;

internal class UserRepository : Repository<UserEntity>, IUserRepository
{
    private readonly DiscussDbContext _db;
    internal UserRepository(DiscussDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task Remove(UserEntity userEntity)
    {
        UserEntity user = await _db.Users
            .Include(u => u.Questions)
            .ThenInclude(q => q.Ratings)
            .Include(u => u.Answers)
            .ThenInclude(a => a.Ratings)
            .Include(u => u.Ratings)
            .FirstOrDefaultAsync(u => u.Id == userEntity.Id);

        _db.Users.Remove(user);
    }
}

