using CarPark.Domain.Entities;

public interface IVehicleService
{
    Task<Vehicle> RegisterVehicle(Vehicle vehicle, CancellationToken ct);
    Task UpdateVehicleStatus(int vehicleId, string newStatus, CancellationToken ct);
}