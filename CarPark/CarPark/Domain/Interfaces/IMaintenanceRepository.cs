using CarPark.Domain.Entities;
namespace CarPark.Domain.Interfaces;

public interface IMaintenanceRepository : IRepository<Maintenance>
{
    Task<IEnumerable<Maintenance>> GetMaintenancesByVehicle(int vehicleId, CancellationToken ct);
    Task<IEnumerable<Maintenance>> GetOverdueMaintenances(DateTime currentDate, CancellationToken ct);
    Task<IEnumerable<Maintenance>> GetMaintenancesByDateRange(DateTime start, DateTime end, CancellationToken ct);
    Task<Maintenance?> GetNextScheduledMaintenance(int vehicleId, CancellationToken ct);
}
