using CarPark.Domain.Entities;
using CarPark.Domain.Interfaces;

public interface ITripRequestRepository : IRepository<TripRequest>
{
    Task<IEnumerable<TripRequest>> GetByUser(int userId, CancellationToken ct);
}