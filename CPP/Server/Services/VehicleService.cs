using CP.Server.DTO;
using CP.Server.Data.Repositories;
using CP.Server.Services.Interfaces;

namespace CP.Server.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepo;

    public VehicleService(IVehicleRepository vehicleRepo)
    {
        _vehicleRepo = vehicleRepo;
    }

    public async Task<List<VehicleDto>> GetVehiclesAsync(string? type = null)
    {
        var vehicles = string.IsNullOrWhiteSpace(type)
            ? await _vehicleRepo.GetAllAsync()
            : await _vehicleRepo.GetByTypeAsync(type);

        return vehicles.Select(v => new VehicleDto
        {
            VehicleId = v.VehicleId,
            Brand = v.Brand,
            Model = v.Model,
            Year = v.Year,
            LicensePlate = v.LicensePlate,
            VehicleType = v.VehicleType ?? "Не указан",
            FuelType = v.FuelType ?? "Не указан",
            Mileage = v.Mileage,
            Status = v.Status ?? "Available",
            DriverName = v.Driver != null ? $"{v.Driver.LastName} {v.Driver.FirstName}" : null
        }).ToList();
    }
}