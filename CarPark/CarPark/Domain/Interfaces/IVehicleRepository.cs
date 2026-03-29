using CarPark.Domain.Entities;
namespace CarPark.Domain.Interfaces;

public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<Vehicle?> GetByLicensePlate(string licensePlate, CancellationToken ct);
    Task<Vehicle?> GetByVin(string vinNumber, CancellationToken ct);
    Task<IEnumerable<Vehicle>> GetVehiclesByStatus(int statusId, CancellationToken ct);
    Task<IEnumerable<Vehicle>> GetVehiclesDueForMaintenance(int mileageThreshold, CancellationToken ct);
    Task UpdateMileage(int vehicleId, int newMileage, CancellationToken ct);
}
