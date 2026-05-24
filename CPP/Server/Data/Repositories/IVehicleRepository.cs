using CP.Server.Models.CarPark;

namespace CP.Server.Data.Repositories;

public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<List<Vehicle>> GetAvailableWithDriverAsync();
    Task<List<Vehicle>> GetByTypeAsync(string type);
}