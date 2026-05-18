using Microsoft.EntityFrameworkCore;
using CP.Server.Data;
using CP.Server.DTO;
using CP.Server.Services.Interfaces;

namespace CP.Server.Services;

public class DriverTripService : IDriverTripService
{
    private readonly CarParkContext _context;

    public DriverTripService(CarParkContext context)
    {
        _context = context;
    }

    public async Task<List<DriverTripDto>> GetDriverTripsAsync(string userId)
    {
        var driver = await _context.Drivers
            .FirstOrDefaultAsync(d => d.UserId == userId);

        if (driver == null)
            return new List<DriverTripDto>();

        return await _context.Trips
            .Include(t => t.Vehicle)
            .Where(t => t.DriverId == driver.DriverId)
            .OrderByDescending(t => t.TripDate)
            .Select(t => new DriverTripDto
            {
                Id = t.TripId,
                ScheduledDate = t.TripDate,
                VehicleName = t.Vehicle.Brand + " " + t.Vehicle.Model,
                Status = t.Status,
                StartOdometer = t.StartOdometer,
                EndOdometer = t.EndOdometer
            })
            .ToListAsync();
    }

    public async Task<bool> StartTripAsync(string userId, int tripId, decimal startOdometer)
    {
        var driver = await _context.Drivers
            .FirstOrDefaultAsync(d => d.UserId == userId);

        if (driver == null)
            return false;

        var trip = await _context.Trips
            .FirstOrDefaultAsync(t =>
                t.TripId == tripId &&
                t.DriverId == driver.DriverId &&
                t.Status == "Scheduled");

        if (trip == null)
            return false;

        trip.StartOdometer = startOdometer;
        trip.StartTime = DateTime.UtcNow;
        trip.Status = "InProgress";

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> EndTripAsync(string userId, int tripId, decimal endOdometer)
    {
        var driver = await _context.Drivers
            .FirstOrDefaultAsync(d => d.UserId == userId);

        if (driver == null)
            return false;

        var trip = await _context.Trips
            .Include(t => t.Vehicle)
            .FirstOrDefaultAsync(t =>
                t.TripId == tripId &&
                t.DriverId == driver.DriverId &&
                t.Status == "InProgress");

        if (trip == null)
        return false;

        trip.EndOdometer = endOdometer;
        trip.EndTime = DateTime.UtcNow;
        trip.Status = "Completed";

        if (trip.Vehicle != null)
        {
            trip.Vehicle.Status = "Available";
        }

        await _context.SaveChangesAsync();

        return true;
    }
}