using CP.Server.Models.CarPark;

namespace CP.Server.Data.Repositories;

public interface IDriverRepository : IRepository<Driver>
{
    Task<Driver?> GetByUserIdAsync(string userId);
    Task<List<Driver>> GetActiveDriversAsync();
    Task<List<Driver>> GetAllWithVehicleAsync();
    Task<Driver?> GetByIdWithVehicleAsync(int id);
}