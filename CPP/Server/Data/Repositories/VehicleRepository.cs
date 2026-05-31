using Microsoft.EntityFrameworkCore;
using CP.Server.Models.CarPark;

namespace CP.Server.Data.Repositories;

public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(CarParkContext context) : base(context) { }

    public async Task<List<Vehicle>> GetAvailableWithDriverAsync()
    {
        return await _dbSet
            .Include(v => v.PrimaryDriver)
            .Where(v => v.Status == "Available")
            .ToListAsync();
    }

    public async Task<List<Vehicle>> GetByTypeAsync(string type)
    {
        return await _dbSet
            .Include(v => v.PrimaryDriver)
            .Where(v => v.VehicleType == type)
            .ToListAsync();
    }

    public async Task<List<Vehicle>> GetAllWithDetailsAsync()
    {
        return await _dbSet
            .Include(v => v.PrimaryDriver)
            .OrderBy(v => v.Brand)
            .ToListAsync();
    }

    public async Task<Vehicle?> GetByIdWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(v => v.PrimaryDriver)
            .FirstOrDefaultAsync(v => v.VehicleId == id);
    }
}