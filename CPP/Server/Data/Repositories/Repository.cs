using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CP.Server.Data.Repositories;

// Репозиторий для работы с сущностями
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly CarParkContext _context;
    protected readonly DbSet<T> _dbSet;

    // Конструктор для инициализации репозитория
    public Repository(CarParkContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    // Метод для получения сущности по ID
    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    // Метод для получения всех сущностей
    public virtual async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    // Метод для поиска сущностей по предикату
    public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    // Метод для добавления сущности
    public virtual async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    // Метод для обновления сущности
    public virtual Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    // Метод для удаления сущности
    public virtual Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    // Метод для сохранения изменений
    public virtual async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}