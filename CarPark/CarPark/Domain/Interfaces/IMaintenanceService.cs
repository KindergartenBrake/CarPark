using CarPark.Domain.Entities;
namespace CarPark.Domain.Interfaces;

public interface IMaintenanceService
{
    Task<Maintenance> ScheduleMaintenance(int vehicleId, int maintenanceTypeId, DateTime plannedDate, CancellationToken ct);
    Task CompleteMaintenance(int maintenanceId, DateTime actualDate, int mileageAtService, decimal cost, string? invoiceNumber, CancellationToken ct);
    Task<IEnumerable<Maintenance>> GetUpcomingMaintenances(int daysAhead, CancellationToken ct);
    Task<bool> IsMaintenanceOverdue(int maintenanceId, CancellationToken ct);
}
