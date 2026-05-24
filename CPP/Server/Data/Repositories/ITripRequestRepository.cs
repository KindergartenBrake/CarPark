using CP.Server.Models.CarPark;

namespace CP.Server.Data.Repositories;

public interface ITripRequestRepository : IRepository<TripRequest>
{
    Task<List<TripRequest>> GetByUserIdAsync(string userId);
    Task<List<TripRequest>> GetAllWithDetailsAsync();
    Task<TripRequest?> GetByIdWithDetailsAsync(int id);
}