using CarPark.Domain.Entities;
using CarPark.Domain.Interfaces;

/// <summary>
/// Controller → DriverService → DriverRepository → База
///  await _repository.Add(driver, ct); - записывает в базу
///  и возвращает созданного водителя
/// </summary>
public class DriverService : IDriverService
{
    private readonly IDriverRepository _repository;

    public DriverService(IDriverRepository repository)
    {
        _repository = repository;
    }

    public async Task<Driver> CreateDriver(Driver driver, CancellationToken ct)
    {
        // БИЗНЕС-ЛОГИКА

        var existing = await _repository.GetByLicenseNumber(driver.LicenseNumber, ct);

        if (existing != null)
            throw new Exception("Водитель с такими правами уже есть");

        await _repository.Add(driver, ct);

        return driver;
    }

    public async Task<Driver?> GetDriver(int id, CancellationToken ct)
    {
        return await _repository.Get(id, ct);
    }

    public async Task<IEnumerable<Driver>> GetAllDrivers(CancellationToken ct)
    {
        return await _repository.GetAll(ct);
    }

    public async Task DeleteDriver(int id, CancellationToken ct)
    {
        var existing = await _repository.Get(id, ct);

        if (existing == null)
            throw new Exception("Водитель не найден");

        await _repository.Delete(id, ct);
    }
}
