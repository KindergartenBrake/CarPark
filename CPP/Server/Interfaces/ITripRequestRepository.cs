using CP.Server.Models.CarPark;

namespace CP.Server.Interfaces;

public interface ITripRequestRepository : IRepository<TripRequest>
{
    Task<IEnumerable<TripRequest>> GetByUser(int userId, CancellationToken ct);
}
