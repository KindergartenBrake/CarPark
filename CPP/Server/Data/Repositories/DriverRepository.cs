using Microsoft.EntityFrameworkCore;
using CP.Server.Models.CarPark;

namespace CP.Server.Data.Repositories;

public class DriverRepository : Repository<Driver>, IDriverRepository
{
    public DriverRepository(CarParkContext context) : base(context) { }

    public async Task<Driver?> GetByUserIdAsync(string userId)
    {
        return await _dbSet.FirstOrDefaultAsync(d => d.UserId == userId);
    }

    public async Task<List<Driver>> GetActiveDriversAsync()
    {
        return await _dbSet.Where(d => d.IsActive).ToListAsync();
    }
}