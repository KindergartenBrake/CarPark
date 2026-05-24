using CP.Server.DTO;

namespace CP.Server.Services.Interfaces;

public interface IVehicleService
{
    Task<List<VehicleDto>> GetVehiclesAsync(string? type = null);
}