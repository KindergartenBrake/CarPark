using CP.Server.Models.CarPark;

namespace CP.Server.Interfaces;

public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<Vehicle?> GetByLicensePlate(string licensePlate, CancellationToken ct);
    Task<Vehicle?> GetByVin(string vinNumber, CancellationToken ct);
    Task<IEnumerable<Vehicle>> GetByStatus(string status, CancellationToken ct);
}
