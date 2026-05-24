using Microsoft.EntityFrameworkCore;
using CP.Server.Models.CarPark;

namespace CP.Server.Data.Repositories;

public class TripRequestRepository : Repository<TripRequest>, ITripRequestRepository
{
    public TripRequestRepository(CarParkContext context) : base(context) { }

    public async Task<List<TripRequest>> GetByUserIdAsync(string userId)
    {
        return await _dbSet
            .Include(r => r.Vehicle)
            .Include(r => r.Driver)
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<TripRequest>> GetAllWithDetailsAsync()
    {
        return await _dbSet
            .Include(r => r.Vehicle)
            .Include(r => r.Driver)
            .Include(r => r.User)
            .Include(r => r.Trip)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<TripRequest?> GetByIdWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(r => r.Vehicle)
            .Include(r => r.Driver)
            .Include(r => r.User)
            .Include(r => r.Trip)
            .FirstOrDefaultAsync(r => r.RequestId == id);
    }
}