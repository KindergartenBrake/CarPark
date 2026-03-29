using CarPark.Domain.Entities;
namespace CarPark.Domain.Interfaces;

public interface IVehicleService
{
    Task<Vehicle> RegisterVehicle(Vehicle vehicle, CancellationToken ct);
    Task UpdateVehicleStatus(int vehicleId, int newStatusId, CancellationToken ct);
    Task<decimal> CalculateTotalMaintenanceCost(int vehicleId, DateTime startDate, DateTime endDate, CancellationToken ct);
    Task<decimal> CalculateTotalFuelCost(int vehicleId, DateTime startDate, DateTime endDate, CancellationToken ct);
    Task<double> CalculateAverageFuelEfficiency(int vehicleId, CancellationToken ct); 
}
