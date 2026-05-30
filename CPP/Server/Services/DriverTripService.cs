using CP.Server.DTO;
using CP.Server.Data.Repositories;
using CP.Server.Services.Interfaces; 

namespace CP.Server.Services;



public class DriverTripService : IDriverTripService
{
    private readonly IDriverRepository _driverRepo;
    private readonly ITripRepository _tripRepo;
    private readonly IVehicleRepository _vehicleRepo;

    public DriverTripService(IDriverRepository driverRepo, ITripRepository tripRepo, IVehicleRepository vehicleRepo)
    {
        _driverRepo = driverRepo;
        _tripRepo = tripRepo;
        _vehicleRepo = vehicleRepo;

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
            // Нормализация статусов
            Status = t.Status?.ToLower() switch
            {
                "scheduled" => "Scheduled",
                "inprogress" => "InProgress",
                "completed" => "Completed",
                _ => "Scheduled"
            },
            StartOdometer = t.StartOdometer,
            EndOdometer = t.EndOdometer
        }).ToList();
    }

    public async Task<bool> StartTripAsync(string userId, int tripId, decimal startOdometer)
    {
        var driver = await _driverRepo.GetByUserIdAsync(userId);
        if (driver == null) return false;

        var trip = await _tripRepo.GetByDriverAndIdAsync(driver.DriverId, tripId);
        // Проверяем оба варианта статуса
        if (trip == null || (trip.Status != "Scheduled" && trip.Status != "scheduled")) 
            return false;

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
        // Проверяем оба варианта статуса
        if (trip == null || trip.DriverId != driver.DriverId || (trip.Status != "InProgress" && trip.Status != "inprogress"))
            return false;

        trip.EndOdometer = endOdometer;
        trip.EndTime = DateTime.UtcNow;
        trip.Status = "Completed";

        if (trip.Vehicle != null)
            trip.Vehicle.Status = "Available";

        await _tripRepo.SaveChangesAsync();
        return true;
    }

    public async Task<DriverVehicleDto?> GetDriverVehicleAsync(string userId)
    {
        var driver = await _driverRepo.GetByUserIdAsync(userId);
        if (driver == null || driver.VehicleId == null)
            return null;

        var vehicle = await _vehicleRepo.GetByIdAsync(driver.VehicleId.Value);
        if (vehicle == null) return null;

        return new DriverVehicleDto
        {
            VehicleId = vehicle.VehicleId,
            Brand = vehicle.Brand,
            Model = vehicle.Model,
            Year = vehicle.Year,
            LicensePlate = vehicle.LicensePlate,
            Vin = vehicle.Vin,
            FuelType = vehicle.FuelType ?? "-",
            VehicleType = vehicle.VehicleType ?? "-",
            Mileage = vehicle.Mileage,
            Status = vehicle.Status ?? "Available",
            InsuranceExpiration = vehicle.InsuranceExpiryDate,
            ImageUrl = null
        };
    }

    public async Task<DriverDashboardDto> GetDriverDashboardAsync(string userId)
    {
        var driver = await _driverRepo.GetByUserIdAsync(userId);
        if (driver == null) return new DriverDashboardDto();

        var allTrips = await _tripRepo.GetByDriverIdAsync(driver.DriverId);
        
        var activeTrip = allTrips.FirstOrDefault(t => t.Status == "InProgress");
        var scheduledTrips = allTrips.Where(t => t.Status == "Scheduled").OrderBy(t => t.TripDate).Take(5).ToList();
        var completedTrips = allTrips.Where(t => t.Status == "Completed").ToList();
        var recentTrips = completedTrips.OrderByDescending(t => t.TripDate).Take(5).ToList();

        return new DriverDashboardDto
        {
            ActiveTrip = activeTrip != null ? new ActiveTripDto
            {
                TripId = activeTrip.TripId,
                Description = activeTrip.TripRequest?.Description ?? "Поездка",
                VehicleName = activeTrip.Vehicle != null ? $"{activeTrip.Vehicle.Brand} {activeTrip.Vehicle.Model}" : "—",
                StartTime = activeTrip.StartTime ?? activeTrip.TripDate,
                StartOdometer = activeTrip.StartOdometer ?? 0
            } : null,
            
            ScheduledTrips = scheduledTrips.Select(t => new ScheduledTripDto
            {
                TripId = t.TripId,
                Description = t.TripRequest?.Description ?? "Поездка",
                VehicleName = t.Vehicle != null ? $"{t.Vehicle.Brand} {t.Vehicle.Model}" : "—",
                ScheduledDate = t.TripDate
            }).ToList(),
            
            RecentTrips = recentTrips.Select(t => new TripHistoryDto
            {
                Date = t.TripDate,
                VehicleName = t.Vehicle != null ? $"{t.Vehicle.Brand} {t.Vehicle.Model}" : "—",
                Mileage = (t.EndOdometer ?? 0) - (t.StartOdometer ?? 0)
            }).ToList(),
            
            CompletedTrips = completedTrips.Count,
            TotalMileage = completedTrips.Sum(t => (t.EndOdometer ?? 0) - (t.StartOdometer ?? 0)),
            TotalDriveTime = "—"
        };
    }
}