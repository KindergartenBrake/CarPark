using CP.Server.DTO;

namespace CP.Server.Services.Interfaces
{
    public interface IAdminTripService
    {
        Task<List<TripDto>> GetAllTripsAsync();
        Task ForceCompleteTripAsync(int tripId, string comment);
        Task CancelTripAsync(int tripId);
        Task<List<string>> GetAvailableDriversAsync();
        Task<List<string>> GetAvailableVehiclesAsync();
        Task RestoreTripAsync(int tripId);
    }
}
