using CP.Server.DTO;

namespace CP.Server.Services.Interfaces;

public interface IDriverTripService
{
    Task<List<DriverTripDto>> GetDriverTripsAsync(string userId);
    Task<bool> StartTripAsync(string userId, int tripId, decimal startOdometer);
    Task<bool> EndTripAsync(string userId, int tripId, decimal endOdometer);
    Task<DriverVehicleDto?> GetDriverVehicleAsync(string userId);
    Task<DriverDashboardDto> GetDriverDashboardAsync(string userId);


}