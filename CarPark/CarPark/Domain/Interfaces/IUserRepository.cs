using CarPark.Domain.Entities;
using CarPark.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsername(string username, CancellationToken ct);
}