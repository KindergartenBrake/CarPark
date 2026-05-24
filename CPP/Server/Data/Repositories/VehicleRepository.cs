using Microsoft.EntityFrameworkCore;
using CP.Server.Models.CarPark;

namespace CP.Server.Data.Repositories;

public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(CarParkContext context) : base(context) { }

    public async Task<List<Vehicle>> GetAvailableWithDriverAsync()
    {
        return await _dbSet
            .Include(v => v.Driver)
            .Where(v => v.Status == "Available")
            .ToListAsync();
    }

    public async Task<List<Vehicle>> GetByTypeAsync(string type)
    {
        return await _dbSet
            .Include(v => v.Driver)
            .Where(v => v.VehicleType == type)
            .ToListAsync();
    }
}