using CP.Server.Models.CarPark;

namespace CP.Server.Interfaces;

public interface ITripRepository : IRepository<Trip>
{
    Task<IEnumerable<Trip>> GetTripsByDriver(int driverId, CancellationToken ct);
    Task<IEnumerable<Trip>> GetTripsByVehicle(int vehicleId, CancellationToken ct);
}
