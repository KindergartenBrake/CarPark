using CP.Server.DTO;
using CP.Server.Data.Repositories;
using CP.Server.Models.CarPark;



namespace CP.Server.Services;

public interface IAdminDriverService
{
    Task<List<AdminDriverDto>> GetAllAsync(string? search = null);
    Task<AdminDriverDto?> GetByIdAsync(int id);
    Task<AdminDriverDto> CreateAsync(CreateDriverDto dto);
    Task UpdateAsync(int id, CreateDriverDto dto);
    Task DeactivateAsync(int id);
}

public class AdminDriverService : IAdminDriverService
{
    private readonly IDriverRepository _driverRepo;
    private readonly IVehicleRepository _vehicleRepo;

    public AdminDriverService(IDriverRepository driverRepo, IVehicleRepository vehicleRepo)
    {
        _driverRepo = driverRepo;
        _vehicleRepo = vehicleRepo;
    }

    public async Task<List<AdminDriverDto>> GetAllAsync(string? search = null)
    {
        var drivers = await _driverRepo.GetAllWithVehicleAsync();

        var query = drivers.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(d =>
                ($"{d.LastName} {d.FirstName} {d.MiddleName}".Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                (d.Phone != null && d.Phone.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                (d.LicenseNumber != null && d.LicenseNumber.Contains(search, StringComparison.OrdinalIgnoreCase)));
        }

        return query.Select(d => new AdminDriverDto
        {
            DriverId = d.DriverId,
            FullName = $"{d.LastName} {d.FirstName} {d.MiddleName}".Trim(),
            LicenseNumber = d.LicenseNumber,
            LicenseExpireDate = d.LicenseExpiryDate,
            Phone = d.Phone,
            IsActive = d.IsActive,
            VehicleName = d.Vehicle != null ? $"{d.Vehicle.Brand} {d.Vehicle.Model} ({d.Vehicle.LicensePlate})" : null,
            IdentityUser = d.UserId
        }).ToList();
    }

    public async Task<AdminDriverDto?> GetByIdAsync(int id)
    {
        var d = await _driverRepo.GetByIdWithVehicleAsync(id);
        if (d == null) return null;

        return new AdminDriverDto
        {
            DriverId = d.DriverId,
            FullName = $"{d.LastName} {d.FirstName} {d.MiddleName}".Trim(),
            LicenseNumber = d.LicenseNumber,
            LicenseExpireDate = d.LicenseExpiryDate,
            Phone = d.Phone,
            IsActive = d.IsActive,
            VehicleName = d.Vehicle != null ? $"{d.Vehicle.Brand} {d.Vehicle.Model} ({d.Vehicle.LicensePlate})" : null,
            IdentityUser = d.UserId
        };
    }

    public async Task<AdminDriverDto> CreateAsync(CreateDriverDto dto)
    {
        var entity = new Driver
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            LicenseNumber = dto.LicenseNumber,
            LicenseExpiryDate = DateTime.SpecifyKind(dto.LicenseExpireDate, DateTimeKind.Utc),
            LicenseIssueDate = DateTime.UtcNow,
            BirthDate = DateTime.UtcNow.AddYears(-30),
            Phone = dto.Phone,
            IsActive = dto.IsActive,
            VehicleId = dto.VehicleId,
            UserId = dto.UserId
        };

        await _driverRepo.AddAsync(entity);
        await _driverRepo.SaveChangesAsync();

        return (await GetByIdAsync(entity.DriverId))!;
    }

    public async Task UpdateAsync(int id, CreateDriverDto dto)
    {
        var entity = await _driverRepo.GetByIdAsync(id);
        if (entity == null) return;

        entity.FirstName = dto.FirstName;
        entity.LastName = dto.LastName;
        entity.MiddleName = dto.MiddleName;
        entity.LicenseNumber = dto.LicenseNumber;
        entity.LicenseExpiryDate = DateTime.SpecifyKind(dto.LicenseExpireDate, DateTimeKind.Utc);
        entity.Phone = dto.Phone;
        entity.IsActive = dto.IsActive;
        entity.VehicleId = dto.VehicleId;
        entity.UserId = dto.UserId;

        await _driverRepo.SaveChangesAsync();
    }

    public async Task DeactivateAsync(int id)
    {
        var entity = await _driverRepo.GetByIdAsync(id);
        if (entity == null) return;
        entity.IsActive = false;
        await _driverRepo.SaveChangesAsync();
    }
}