
namespace CarPark.Domain.Interfaces;
/// <summary>
/// База
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : class
{
    Task Add(T item, CancellationToken cancellationToken);
    Task<T?> Get(int id, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
}
