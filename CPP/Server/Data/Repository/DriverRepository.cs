using CP.Server.Interfaces;
using CP.Server.Models.CarPark;

namespace CP.Server.Data.Repository;

public class DriverRepository : IDriverRepository
{
    private readonly List<Driver> _drivers = []; // тут должна быть реальная база данных вместо List<Driver>. 

    public Task Add(Driver item, CancellationToken ct)
    {
        _drivers.Add(item);
        return Task.CompletedTask;
    }

    public Task<Driver?> Get(int id, CancellationToken ct)
    {
        return Task.FromResult(_drivers.FirstOrDefault(x => x.DriverId == id));
    }

    public Task<IEnumerable<Driver>> GetAll(CancellationToken ct)
    {
        return Task.FromResult(_drivers.AsEnumerable());
    }

    public Task Delete(int id, CancellationToken ct)
    {
        var item = _drivers.FirstOrDefault(x => x.DriverId == id);
        if (item != null)
            _drivers.Remove(item);

        return Task.CompletedTask;
    }

    public Task<Driver?> GetByLicenseNumber(string licenseNumber, CancellationToken ct)
    {
        return Task.FromResult(_drivers.FirstOrDefault(x => x.LicenseNumber == licenseNumber));
    }

    public Task<IEnumerable<Driver>> GetActiveDrivers(CancellationToken ct)
    {
        return Task.FromResult(_drivers.Where(x => x.IsActive));
    }

    public Task<IEnumerable<Driver>> GetDriversWithExpiredLicenses(DateTime currentDate, CancellationToken ct)
    {
        return Task.FromResult(_drivers.Where(x => x.LicenseExpiryDate < currentDate));
    }
}