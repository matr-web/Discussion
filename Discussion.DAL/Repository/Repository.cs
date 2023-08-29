using Discussion.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Discussion.DAL.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DiscussDbContext _context;
    
    // Separate property for DbSet type because we are using Generic Class. 
    private DbSet<T> _dbSet;

    public Repository(DiscussDbContext context)
    {
        _context = context;

        // Set DbSet for given Type T.
        // Example: if T is 'Category'
        // _dbSet == _context.Category.
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, string includeProperties = null)
    {
        IQueryable<T> query = _dbSet;

        // If the predicate is not null, get just the element's which fulfill given requirement's.
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        // If we have some Include Properties and the searched element's exists.
        if (includeProperties != null && query != null)
        {
            // Load the given Include Properties on given element's.
            foreach (var include in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(include);
            }
        }

        // Return the searched element's as List.
        return await query.ToListAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, string includeProperties = null)
    {
        IQueryable<T> query = _dbSet;

        // Get the searched element.
        var searchedElement = await query.FirstOrDefaultAsync(predicate);
 
        // If we have some Include Properties and the searched element exists.
        if (includeProperties != null && searchedElement != null)
        {
            // Load the given Include Properties on given element.
            foreach (var include in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                searchedElement = query.Where(q => q == searchedElement).Include(include).FirstOrDefault();
            }
        }

        // Return the searched element or his default value.
        return searchedElement;
    }

    public async Task AddAsync(T entity)
    {
        await Task.FromResult(_dbSet.AddAsync(entity));
    }

    public async Task UpdateAsync(T entity)
    {
        await Task.FromResult(_context.Entry(entity).State = EntityState.Modified);
    }

    public Task Remove(T entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public Task RemoveRange(IEnumerable<T> entity)
    {
        _dbSet.RemoveRange(entity);
        return Task.CompletedTask;
    }
}
