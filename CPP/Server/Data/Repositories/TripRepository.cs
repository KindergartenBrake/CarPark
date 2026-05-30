using Microsoft.EntityFrameworkCore;
using CP.Server.Models.CarPark;

namespace CP.Server.Data.Repositories;

public class TripRepository : Repository<Trip>, ITripRepository
{
    public TripRepository(CarParkContext context) : base(context) { }

    public async Task<List<Trip>> GetByDriverIdAsync(int driverId)
    {
        return await _dbSet
            .Include(t => t.Vehicle)
            .Where(t => t.DriverId == driverId)
            .OrderByDescending(t => t.TripDate)
            .ToListAsync();
    }

    public async Task<Trip?> GetByDriverAndIdAsync(int driverId, int tripId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(t => t.TripId == tripId && t.DriverId == driverId);
    }

    public async Task<Trip?> GetWithVehicleAsync(int tripId)
    {
        return await _dbSet
            .Include(t => t.Vehicle)
            .FirstOrDefaultAsync(t => t.TripId == tripId);
    }

    public async Task<List<Trip>> GetAllTripsWithDetailsAsync()
    {
        return await _dbSet
            .Include(t => t.Driver)
            .Include(t => t.Vehicle)
            .Include(t => t.TripRequest)
            .OrderByDescending(t => t.TripDate)
            .ToListAsync();
    }
}