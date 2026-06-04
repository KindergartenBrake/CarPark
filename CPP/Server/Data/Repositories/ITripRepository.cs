using CP.Server.Models.CarPark;

namespace CP.Server.Data.Repositories;

public interface ITripRepository : IRepository<Trip>
{
    Task<List<Trip>> GetByDriverIdAsync(int driverId);
    Task<Trip?> GetByDriverAndIdAsync(int driverId, int tripId);
    Task<Trip?> GetWithVehicleAsync(int tripId);
    Task<List<Trip>> GetAllTripsWithDetailsAsync();

    Task<Trip?> GetByRequestIdAsync(int requestId);
}