using Microsoft.EntityFrameworkCore;
using CP.Server.Models.CarPark;

namespace CP.Server.Data.Repositories;

// Репозиторий для транспортных средств
public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(CarParkContext context) : base(context) { }

    // Метод для получения доступных транспортных средств с водителем
    public async Task<List<Vehicle>> GetAvailableWithDriverAsync()
    {
        return await _dbSet
            .Include(v => v.PrimaryDriver)
            .Where(v => v.Status == "Available")
            .ToListAsync();
    }

    // Метод для получения транспортных средств по типу
    public async Task<List<Vehicle>> GetByTypeAsync(string type)
    {
        return await _dbSet
            .Include(v => v.PrimaryDriver)
            .Where(v => v.VehicleType == type)
            .ToListAsync();
    }

    // Метод для получения всех транспортных средств с деталями
    public async Task<List<Vehicle>> GetAllWithDetailsAsync()
    {
        return await _dbSet
            .Include(v => v.PrimaryDriver)
            .OrderBy(v => v.Brand)
            .ToListAsync();
    }

    // Метод для получения транспортного средства по ID с деталями
    public async Task<Vehicle?> GetByIdWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(v => v.PrimaryDriver)
            .FirstOrDefaultAsync(v => v.VehicleId == id);
    }
}