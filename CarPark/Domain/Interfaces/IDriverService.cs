namespace CarPark.Domain.Interfaces;
using CarPark.Domain.Entities;


public interface IDriverService
{
    Task<Driver> CreateDriver(Driver driver, CancellationToken ct);
    Task<Driver?> GetDriver(int id, CancellationToken ct);
    Task<IEnumerable<Driver>> GetAllDrivers(CancellationToken ct);
    Task DeleteDriver(int id, CancellationToken ct);
}
