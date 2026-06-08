using Microsoft.EntityFrameworkCore;
using CP.Server.Models.CarPark;

namespace CP.Server.Data.Repositories;

// Репозиторий для заявок на поездки
public class TripRequestRepository : Repository<TripRequest>, ITripRequestRepository
{
    public TripRequestRepository(CarParkContext context) : base(context) { }

    // Метод для получения заявок на поездки по ID пользователя
    public async Task<List<TripRequest>> GetByUserIdAsync(string userId)
    {
        return await _dbSet
            .Include(r => r.Vehicle)
            .Include(r => r.Driver)
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    // Метод для получения всех заявок на поездки с деталями
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

    // Метод для получения заявки на поездку по ID с деталями
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