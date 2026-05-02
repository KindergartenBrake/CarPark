using CarPark.Domain.Entities;

namespace CarPark.Domain.Interfaces;

public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<Vehicle?> GetByLicensePlate(string licensePlate, CancellationToken ct);
    Task<Vehicle?> GetByVin(string vinNumber, CancellationToken ct);
    Task<IEnumerable<Vehicle>> GetByStatus(string status, CancellationToken ct);
}