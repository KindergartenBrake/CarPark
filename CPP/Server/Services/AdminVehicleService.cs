using CP.Server.DTO;
using CP.Server.Data.Repositories;
using CP.Server.Models.CarPark;

namespace CP.Server.Services;

public interface IAdminVehicleService
{
    Task<List<AdminVehicleDto>> GetAllVehiclesAsync(string? search = null);
    Task<AdminVehicleDto?> GetByIdAsync(int id);
    Task<AdminVehicleDto> CreateAsync(CreateVehicleDto dto);
    Task UpdateAsync(int id, CreateVehicleDto dto);
    Task DeleteAsync(int id);
    Task DeactivateAsync(int id);
}

public class AdminVehicleService : IAdminVehicleService
{
    private readonly IVehicleRepository _vehicleRepo;
    private readonly IDriverRepository _driverRepo;

    public AdminVehicleService(IVehicleRepository vehicleRepo, IDriverRepository driverRepo)
    {
        _vehicleRepo = vehicleRepo;
        _driverRepo = driverRepo;
    }

    public async Task<List<AdminVehicleDto>> GetAllVehiclesAsync(string? search = null)
    {
        var vehicles = await _vehicleRepo.GetAllWithDetailsAsync();

        var query = vehicles.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(v =>
                (v.Brand != null && v.Brand.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                (v.Model != null && v.Model.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                (v.LicensePlate != null && v.LicensePlate.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                (v.Vin != null && v.Vin.Contains(search, StringComparison.OrdinalIgnoreCase)));
        }

        return query.Select(v => new AdminVehicleDto
        {
            VehicleId = v.VehicleId,
            Brand = v.Brand,
            Model = v.Model,
            Year = v.Year,
            LicensePlate = v.LicensePlate,
            Vin = v.Vin,
            FuelType = v.FuelType ?? "-",
            VehicleType = v.VehicleType ?? "-",
            Mileage = v.Mileage,
            Status = v.Status ?? "Available",
            InsuranceExpire = v.InsuranceExpiryDate,
            DriverName = v.PrimaryDriver != null ? $"{v.PrimaryDriver.LastName} {v.PrimaryDriver.FirstName}" : null,
            DriverId = v.DriverId,
            HasActiveTrips = v.Trips.Any(t => t.Status == "Scheduled" || t.Status == "InProgress"),
            Trips = v.Trips.Select(t => new AdminTripDto
            {
                TripId = t.TripId,
                Route = $"Поездка #{t.TripId}",
                Distance = t.EndOdometer.HasValue && t.StartOdometer.HasValue 
                    ? t.EndOdometer.Value - t.StartOdometer.Value 
                    : 0
            }).ToList()
        }).ToList();
    }

    public async Task<AdminVehicleDto?> GetByIdAsync(int id)
    {
        var v = await _vehicleRepo.GetByIdWithDetailsAsync(id);
        if (v == null) return null;

        return new AdminVehicleDto
        {
            VehicleId = v.VehicleId,
            Brand = v.Brand,
            Model = v.Model,
            Year = v.Year,
            LicensePlate = v.LicensePlate,
            Vin = v.Vin,
            FuelType = v.FuelType ?? "-",
            VehicleType = v.VehicleType ?? "-",
            Mileage = v.Mileage,
            Status = v.Status ?? "Available",
            InsuranceExpire = v.InsuranceExpiryDate,
            DriverName = v.PrimaryDriver != null ? $"{v.PrimaryDriver.LastName} {v.PrimaryDriver.FirstName}" : "Не назначен",
            DriverId = v.DriverId,
            HasActiveTrips = v.Trips.Any(t => t.Status == "Scheduled" || t.Status == "InProgress"),
            Trips = v.Trips.Select(t => new AdminTripDto
            {
                TripId = t.TripId,
                Route = $"Поездка #{t.TripId}",
                Distance = t.EndOdometer.HasValue && t.StartOdometer.HasValue 
                    ? t.EndOdometer.Value - t.StartOdometer.Value 
                    : 0
            }).ToList()
        };
    }

    public async Task<AdminVehicleDto> CreateAsync(CreateVehicleDto dto)
    {
        var entity = new Vehicle
        {
            Brand = dto.Brand,
            Model = dto.Model,
            Year = dto.Year,
            LicensePlate = dto.LicensePlate,
            Vin = dto.Vin,
            FuelType = dto.FuelType,
            VehicleType = dto.VehicleType,
            Mileage = dto.Mileage,
            Status = dto.Status,
            InsuranceExpiryDate = dto.InsuranceExpire,
            DriverId = dto.DriverId
        };

        await _vehicleRepo.AddAsync(entity);
        await _vehicleRepo.SaveChangesAsync();

        return await GetByIdAsync(entity.VehicleId) ?? new AdminVehicleDto();
    }

    public async Task UpdateAsync(int id, CreateVehicleDto dto)
    {
        var entity = await _vehicleRepo.GetByIdAsync(id);
        if (entity == null) return;

        entity.Brand = dto.Brand;
        entity.Model = dto.Model;
        entity.Year = dto.Year;
        entity.LicensePlate = dto.LicensePlate;
        entity.Vin = dto.Vin;
        entity.FuelType = dto.FuelType;
        entity.VehicleType = dto.VehicleType;
        entity.Mileage = dto.Mileage;
        entity.Status = dto.Status;
        entity.InsuranceExpiryDate = dto.InsuranceExpire;
        entity.DriverId = dto.DriverId;

        await _vehicleRepo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _vehicleRepo.GetByIdAsync(id);
        if (entity == null) return;

        await _vehicleRepo.DeleteAsync(entity);
        await _vehicleRepo.SaveChangesAsync();
    }

    public async Task DeactivateAsync(int id)
    {
        var entity = await _vehicleRepo.GetByIdAsync(id);
        if (entity == null) return;

        entity.Status = "Decommissioned";
        await _vehicleRepo.SaveChangesAsync();
    }
}