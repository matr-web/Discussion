using Discussion.DAL.Repository.IRepository;
using Discussion.Entities;

namespace Discussion.DAL.Repository;

internal class CategoryRepository : Repository<CategoryEntity>, ICategoryRepository
{
    internal CategoryRepository(DiscussDbContext db) : base(db)
    {

    }
}
