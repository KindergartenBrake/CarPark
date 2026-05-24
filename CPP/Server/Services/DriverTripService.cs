using CP.Server.DTO;
using CP.Server.Data.Repositories;
using CP.Server.Services.Interfaces; 

namespace CP.Server.Services;



public class DriverTripService : IDriverTripService
{
    private readonly IDriverRepository _driverRepo;
    private readonly ITripRepository _tripRepo;

    public DriverTripService(IDriverRepository driverRepo, ITripRepository tripRepo)
    {
        _driverRepo = driverRepo;
        _tripRepo = tripRepo;
    }

    public async Task<List<DriverTripDto>> GetDriverTripsAsync(string userId)
    {
        var driver = await _driverRepo.GetByUserIdAsync(userId);
        if (driver == null)
            return new List<DriverTripDto>();

        var trips = await _tripRepo.GetByDriverIdAsync(driver.DriverId);

        return trips.Select(t => new DriverTripDto
        {
            Id = t.TripId,
            ScheduledDate = t.TripDate,
            VehicleName = t.Vehicle != null ? $"{t.Vehicle.Brand} {t.Vehicle.Model}" : "Не назначен",
            Status = t.Status,
            StartOdometer = t.StartOdometer,
            EndOdometer = t.EndOdometer
        }).ToList();
    }

    public async Task<bool> StartTripAsync(string userId, int tripId, decimal startOdometer)
    {
        var driver = await _driverRepo.GetByUserIdAsync(userId);
        if (driver == null) return false;

        var trip = await _tripRepo.GetByDriverAndIdAsync(driver.DriverId, tripId);
        if (trip == null || trip.Status != "Scheduled") return false;

        trip.StartOdometer = startOdometer;
        trip.StartTime = DateTime.UtcNow;
        trip.Status = "InProgress";

        await _tripRepo.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EndTripAsync(string userId, int tripId, decimal endOdometer)
    {
        var driver = await _driverRepo.GetByUserIdAsync(userId);
        if (driver == null) return false;

        var trip = await _tripRepo.GetWithVehicleAsync(tripId);
        if (trip == null || trip.DriverId != driver.DriverId || trip.Status != "InProgress")
            return false;

        trip.EndOdometer = endOdometer;
        trip.EndTime = DateTime.UtcNow;
        trip.Status = "Completed";

        if (trip.Vehicle != null)
            trip.Vehicle.Status = "Available";

        await _tripRepo.SaveChangesAsync();
        return true;
    }
}