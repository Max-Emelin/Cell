using Cell.Domain.Interfaces;

namespace Cell.DAL;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IQueryable<TEntity> GetAll()
    {
        return _context.Set<TEntity>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity", "Параметр \"Entity\" пуст");
        }

        _context.Add(entity);
        await _context.SaveChangesAsync();

        return await Task.FromResult(entity);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity", "Параметр \"Entity\" пуст");
        }

        _context.Update(entity);
        await _context.SaveChangesAsync();

        return await Task.FromResult(entity);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<TEntity> RemoveAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity", "Параметр \"Entity\" пуст");
        }

        _context.Remove(entity);
        await _context.SaveChangesAsync();

        return await Task.FromResult(entity);
    }
}