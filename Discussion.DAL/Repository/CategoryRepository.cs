using Discussion.DAL.Repository.IRepository;
using Discussion.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discussion.DAL.Repository;

internal class CategoryRepository : Repository<CategoryEntity>, ICategoryRepository
{
    private readonly DiscussDbContext _db;
    internal CategoryRepository(DiscussDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task Remove(CategoryEntity categoryEntity)
    {
        CategoryEntity category = await _db.Categories
            .Include(c => c.Questions)
            .ThenInclude(q => q.Answers)
            .ThenInclude(a => a.Ratings)
            .Include(c => c.Questions)
            .ThenInclude(q => q.Ratings)
            .FirstOrDefaultAsync(c => c.Id == categoryEntity.Id);

        _db.Categories.Remove(category);
    }
}
