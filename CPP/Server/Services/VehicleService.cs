using CP.Server.Data;
using CP.Server.DTO;
using Microsoft.EntityFrameworkCore;

namespace CP.Server.Services;

public interface IVehicleService
{
    Task<List<VehicleDto>> GetVehiclesAsync(string? type = null);
}

public class VehicleService : IVehicleService
{
    private readonly CarParkContext _context;
    
    public VehicleService(CarParkContext context)
    {
        _context = context;
    }

    public async Task<List<VehicleDto>> GetVehiclesAsync(string? type = null)
    {
        var query = _context.Vehicles
            .Include(v => v.Driver)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(type))
            query = query.Where(v => v.VehicleType == type);

        return await query.Select(v => new VehicleDto
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
        }).ToListAsync();
    }
}