using CarPark.Domain.Entities;
namespace CarPark.Domain.Interfaces;

public interface IRefuelingRepository : IRepository<Refueling>
{
    Task<IEnumerable<Refueling>> GetRefuelingsByVehicle(int vehicleId, CancellationToken ct);
    Task<IEnumerable<Refueling>> GetRefuelingsByDriver(int driverId, CancellationToken ct);
    Task<decimal> GetTotalFuelCostByVehicle(int vehicleId, DateTime startDate, DateTime endDate, CancellationToken ct);
    Task<decimal> GetAverageFuelConsumption(int vehicleId, CancellationToken ct); 
}
