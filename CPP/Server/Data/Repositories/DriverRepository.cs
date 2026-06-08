using Microsoft.EntityFrameworkCore;
using CP.Server.Models.CarPark;

namespace CP.Server.Data.Repositories;

// Репозиторий для водителей
public class DriverRepository : Repository<Driver>, IDriverRepository
{
    public DriverRepository(CarParkContext context) : base(context) { }

    // Метод для получения водителя по ID пользователя
    public async Task<Driver?> GetByUserIdAsync(string userId)
    {
        return await _dbSet.FirstOrDefaultAsync(d => d.UserId == userId);
    }

    // Метод для получения активных водителей
    public async Task<List<Driver>> GetActiveDriversAsync()
    {
        return await _dbSet.Where(d => d.IsActive).ToListAsync();
    }

    // Метод для получения всех водителей с транспортным средством
    public async Task<List<Driver>> GetAllWithVehicleAsync()
    {
        return await _dbSet
            .Include(d => d.Vehicle)
            .OrderBy(d => d.LastName)
            .ToListAsync();
    }
    // Метод для получения водителя по ID с транспортным средством
    public async Task<Driver?> GetByIdWithVehicleAsync(int id)
    {
        return await _dbSet
            .Include(d => d.Vehicle)
            .FirstOrDefaultAsync(d => d.DriverId == id);
    }
}