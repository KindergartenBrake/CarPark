using CP.Server.DTO;

namespace CP.Server.Services.Interfaces
{
    public interface ITripRequestService
    {
        Task<List<TripRequestDto>> GetMyRequestsAsync(string userId);
        Task<TripRequestDto?> GetByIdAsync(int id);
        Task<TripRequestDto> CreateAsync(CreateTripRequestDto dto, string userId);
        Task<List<TripRequestForAdminDto>> GetAllRequestsAsync();
        Task<bool> AssignTripAsync(int requestId, int vehicleId, int driverId);
        Task<bool> RejectTripAsync(int requestId, string? reason);
        Task<List<AvailableVehicleDto>> GetAvailableVehiclesAsync();
        Task<List<AvailableDriverDto>> GetAvailableDriversAsync();
        Task<EmployeeDashboardDto> GetEmployeeDashboardAsync(string userId);
    }
}
