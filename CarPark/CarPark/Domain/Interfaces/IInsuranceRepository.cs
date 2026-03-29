using CarPark.Domain.Entities;
namespace CarPark.Domain.Interfaces;

public interface IInsuranceRepository : IRepository<Insurance>
{
    Task<IEnumerable<Insurance>> GetInsurancesByVehicle(int vehicleId, CancellationToken ct);
    Task<IEnumerable<Insurance>> GetExpiredInsurances(DateTime currentDate, CancellationToken ct);
    Task<IEnumerable<Insurance>> GetInsurancesExpiringSoon(DateTime currentDate, int daysThreshold, CancellationToken ct);
}
