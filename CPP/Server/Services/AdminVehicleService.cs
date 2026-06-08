using CP.Server.DTO;
using CP.Server.Data.Repositories;
using CP.Server.Models.CarPark;
using CP.Server.Services.Interfaces;
namespace CP.Server.Services;


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

        // Валидация обязательных полей
        if (string.IsNullOrWhiteSpace(dto.Brand))
            throw new ArgumentException("Марка обязательна");
        if (string.IsNullOrWhiteSpace(dto.Model))
            throw new ArgumentException("Модель обязательна");
        if (string.IsNullOrWhiteSpace(dto.LicensePlate))
            throw new ArgumentException("Госномер обязателен");

        // Проверка уникальности госномера
        var existingVehicle = await _vehicleRepo.FindAsync(v => v.LicensePlate == dto.LicensePlate);
        if (existingVehicle.Any())
            throw new InvalidOperationException("Автомобиль с таким госномером уже существует");

        // Проверка VIN (если нужен)
        if (string.IsNullOrWhiteSpace(dto.Vin))
            throw new ArgumentException("VIN обязателен");

        // Проверка уникальности VIN
        var existingVin = await _vehicleRepo.FindAsync(v => v.Vin == dto.Vin);
        if (existingVin.Any())
            throw new InvalidOperationException("Автомобиль с таким VIN уже существует");

        // Проверка года выпуска
        if (dto.Year < 1900 || dto.Year > DateTime.UtcNow.Year + 1)
            throw new ArgumentException("Некорректный год выпуска");

        // Проверка пробега
        if (dto.Mileage < 0)
            throw new ArgumentException("Пробег не может быть отрицательным");

        // Проверка страховки
        if (dto.InsuranceExpire < DateTime.UtcNow.Date)
            throw new ArgumentException("Страховка не может быть просрочена");   
        
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

        var oldDriverId = entity.DriverId;

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

        // Обновляем связь с водителем
        if (oldDriverId != dto.DriverId)
        {
            // Очищаем VehicleId у старого водителя
            if (oldDriverId.HasValue)
            {
                var oldDriver = await _driverRepo.GetByIdAsync(oldDriverId.Value);
                if (oldDriver != null)
                {
                    oldDriver.VehicleId = null;
                    await _driverRepo.SaveChangesAsync();
                }
            }

            // Назначаем VehicleId у нового водителя
            if (dto.DriverId.HasValue)
            {
                var newDriver = await _driverRepo.GetByIdAsync(dto.DriverId.Value);
                if (newDriver != null)
                {
                    newDriver.VehicleId = id;
                    await _driverRepo.SaveChangesAsync();
                }
            }
        }
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