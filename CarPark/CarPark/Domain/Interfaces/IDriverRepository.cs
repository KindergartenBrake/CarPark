using CarPark.Domain.Entities;
namespace CarPark.Domain.Interfaces;

public interface IDriverRepository : IRepository<Driver>
{
    Task<Driver?> GetByLicenseNumber(string licenseNumber, CancellationToken ct);
    Task<IEnumerable<Driver>> GetActiveDrivers(CancellationToken ct);
    Task<IEnumerable<Driver>> GetDriversWithExpiredLicenses(DateTime currentDate, CancellationToken ct);
}
