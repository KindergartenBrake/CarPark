using CP.Server.DTO;
using CP.Server.Data.Repositories;
using CP.Server.Services.Interfaces;
namespace CP.Server.Services;



public class AdminTripService : IAdminTripService
{
    private readonly ITripRepository _tripRepo;
    private readonly IDriverRepository _driverRepo;
    private readonly IVehicleRepository _vehicleRepo;
    private readonly ITripRequestRepository _tripRequestRepo;

    public AdminTripService(
        ITripRepository tripRepo,
        IDriverRepository driverRepo,
        IVehicleRepository vehicleRepo,
        ITripRequestRepository tripRequestRepo)
    {
        _tripRepo = tripRepo;
        _driverRepo = driverRepo;
        _vehicleRepo = vehicleRepo;
        _tripRequestRepo = tripRequestRepo;
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
            // Преобразуем DateTime? в TimeSpan? (только время)
            StartTime = t.StartTime.HasValue ? TimeOnly.FromDateTime(t.StartTime.Value).ToTimeSpan() : (TimeSpan?)null,
            EndTime = t.EndTime.HasValue ? TimeOnly.FromDateTime(t.EndTime.Value).ToTimeSpan() : (TimeSpan?)null,
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

    public async Task RestoreTripAsync(int tripId)
    {
        var trip = await _tripRepo.GetByIdAsync(tripId);
        if (trip == null) 
            throw new Exception("Поездка не найдена");
        
        if (trip.Status != "Cancelled")
            throw new Exception("Можно восстановить только отменённые поездки");
        
        trip.Status = "Scheduled";
        
        var request = await _tripRequestRepo.GetByIdAsync(trip.RequestId);
        if (request != null)
        {
            request.Status = "Approved";
        }
        
        if (trip.VehicleId > 0)
        {
            var vehicle = await _vehicleRepo.GetByIdAsync(trip.VehicleId);
            if (vehicle != null && vehicle.Status == "Busy")
            {
                vehicle.Status = "Available";
            }
        }
        
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