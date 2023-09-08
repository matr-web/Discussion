using Discussion.Entities;

namespace Discussion.DAL.Repository.IRepository;

public interface ICategoryRepository : IRepository<CategoryEntity>
{
    new Task Remove(CategoryEntity categoryEntity);
}
