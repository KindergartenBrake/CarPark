using Microsoft.EntityFrameworkCore;
using CP.Server.Models.CarPark;

namespace CP.Server.Data.Repositories;

// Репозиторий для поездок
public class TripRepository : Repository<Trip>, ITripRepository
{
    public TripRepository(CarParkContext context) : base(context) { }

    // Метод для получения поездок по ID водителя
    public async Task<List<Trip>> GetByDriverIdAsync(int driverId)
    {
        return await _dbSet
            .Include(t => t.Vehicle)
            .Where(t => t.DriverId == driverId)
            .OrderByDescending(t => t.TripDate)
            .ToListAsync();
    }

    // Метод для получения поездки по ID водителя и ID поездки
    public async Task<Trip?> GetByDriverAndIdAsync(int driverId, int tripId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(t => t.TripId == tripId && t.DriverId == driverId);
    }

    // Метод для получения поездки с транспортным средством
    public async Task<Trip?> GetWithVehicleAsync(int tripId)
    {
        return await _dbSet
            .Include(t => t.Vehicle)
            .FirstOrDefaultAsync(t => t.TripId == tripId);
    }

    // Метод для получения всех поездок с деталями
    public async Task<List<Trip>> GetAllTripsWithDetailsAsync()
    {
        return await _dbSet
            .Include(t => t.Driver)
            .Include(t => t.Vehicle)
            .Include(t => t.TripRequest)
            .OrderByDescending(t => t.TripDate)
            .ToListAsync();
    }
    // Метод для получения поездки по ID заявки на поездку
    public async Task<Trip?> GetByRequestIdAsync(int requestId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(t => t.RequestId == requestId);
    }
}