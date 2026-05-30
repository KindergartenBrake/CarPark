using CP.Server.DTO;
using CP.Server.Data.Repositories;

namespace CP.Server.Services;

public interface IAdminTripService
{
    Task<List<TripDto>> GetAllTripsAsync();
    Task ForceCompleteTripAsync(int tripId, string comment);
    Task CancelTripAsync(int tripId);
    Task<List<string>> GetAvailableDriversAsync();
    Task<List<string>> GetAvailableVehiclesAsync();
}

public class AdminTripService : IAdminTripService
{
    private readonly ITripRepository _tripRepo;
    private readonly IDriverRepository _driverRepo;
    private readonly IVehicleRepository _vehicleRepo;

    public AdminTripService(
        ITripRepository tripRepo,
        IDriverRepository driverRepo,
        IVehicleRepository vehicleRepo)
    {
        _tripRepo = tripRepo;
        _driverRepo = driverRepo;
        _vehicleRepo = vehicleRepo;
    }

    public async Task<List<TripDto>> GetAllTripsAsync()
    {
        var trips = await _tripRepo.GetAllTripsWithDetailsAsync();
        
        return trips.Select(t => new TripDto
        {
            TripId = t.TripId,
            RequestId = t.RequestId,
            DriverName = t.Driver != null ? $"{t.Driver.LastName} {t.Driver.FirstName}" : "—",
            VehicleName = t.Vehicle != null ? $"{t.Vehicle.Brand} {t.Vehicle.Model}" : "—",
            TripDate = t.TripDate,
            StartTime = t.StartTime,
            EndTime = t.EndTime,
            StartOdometer = t.StartOdometer,
            EndOdometer = t.EndOdometer,
            Status = t.Status ?? "Scheduled",
            Route = t.TripRequest?.Description ?? "—",
            Comment = null
        }).ToList();
    }

    public async Task ForceCompleteTripAsync(int tripId, string comment)
    {
        var trip = await _tripRepo.GetByIdAsync(tripId);
        if (trip == null) throw new Exception("Поездка не найдена");
        
        trip.Status = "Completed";
        trip.EndTime = DateTime.UtcNow;
        await _tripRepo.SaveChangesAsync();
    }

    public async Task CancelTripAsync(int tripId)
    {
        var trip = await _tripRepo.GetByIdAsync(tripId);
        if (trip == null) throw new Exception("Поездка не найдена");
        
        trip.Status = "Cancelled";
        await _tripRepo.SaveChangesAsync();
    }

    public async Task<List<string>> GetAvailableDriversAsync()
    {
        var drivers = await _driverRepo.GetActiveDriversAsync();
        return drivers.Select(d => $"{d.LastName} {d.FirstName}").ToList();
    }

    public async Task<List<string>> GetAvailableVehiclesAsync()
    {
        var vehicles = await _vehicleRepo.GetAllAsync();
        return vehicles.Select(v => $"{v.Brand} {v.Model}").ToList();
    }
}