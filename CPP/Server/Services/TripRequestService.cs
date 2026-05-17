using CP.Server.Data;
using CP.Server.DTO;
using Microsoft.EntityFrameworkCore;

namespace CP.Server.Services;

public interface ITripRequestService
{
    Task<List<TripRequestDto>> GetMyRequestsAsync(string userId);
    Task<TripRequestDto?> GetByIdAsync(int id);
    Task<TripRequestDto> CreateAsync(CreateTripRequestDto dto, string userId);
}

public class TripRequestService : ITripRequestService
{
    private readonly CarParkContext _context;

    public TripRequestService(CarParkContext context)
    {
        _context = context;
    }

    public async Task<List<TripRequestDto>> GetMyRequestsAsync(string userId)
    {
        return await _context.TripRequests
            .Include(r => r.Vehicle)
            .Include(r => r.Driver)
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new TripRequestDto
            {
                RequestId = r.RequestId,
                CreatedAt = r.CreatedAt,
                TripDate = r.TripDate,
                StartTime = r.StartTime,
                EndTime = r.EndTime,
                VehicleType = r.VehicleType,
                Status = r.Status,
                Description = r.Description,
                RejectionReason = r.RejectionReason,
                VehicleBrand = r.Vehicle != null ? r.Vehicle.Brand : null,
                VehicleModel = r.Vehicle != null ? r.Vehicle.Model : null,
                LicensePlate = r.Vehicle != null ? r.Vehicle.LicensePlate : null,
                DriverName = r.Driver != null ? $"{r.Driver.LastName} {r.Driver.FirstName}" : null,
                DriverPhone = r.Driver != null ? r.Driver.Phone : null
            })
            .ToListAsync();
    }

    public async Task<TripRequestDto?> GetByIdAsync(int id)
    {
        return await _context.TripRequests
            .Include(r => r.Vehicle)
            .Include(r => r.Driver)
            .Where(r => r.RequestId == id)
            .Select(r => new TripRequestDto
            {
                RequestId = r.RequestId,
                CreatedAt = r.CreatedAt,
                TripDate = r.TripDate,
                StartTime = r.StartTime,
                EndTime = r.EndTime,
                VehicleType = r.VehicleType,
                Status = r.Status,
                Description = r.Description,
                RejectionReason = r.RejectionReason,
                VehicleBrand = r.Vehicle != null ? r.Vehicle.Brand : null,
                VehicleModel = r.Vehicle != null ? r.Vehicle.Model : null,
                LicensePlate = r.Vehicle != null ? r.Vehicle.LicensePlate : null,
                DriverName = r.Driver != null ? $"{r.Driver.LastName} {r.Driver.FirstName}" : null,
                DriverPhone = r.Driver != null ? r.Driver.Phone : null
            })
            .FirstOrDefaultAsync();
    }

    public async Task<TripRequestDto> CreateAsync(CreateTripRequestDto dto, string userId)
    {
        var entity = new Models.CarPark.TripRequest
        {
            UserId = userId,
            VehicleType = dto.VehicleType,
            TripDate = DateTime.SpecifyKind(dto.TripDate, DateTimeKind.Utc),
            StartTime = dto.StartTime.HasValue ? DateTime.SpecifyKind(dto.StartTime.Value, DateTimeKind.Utc) : null,
            EndTime = dto.EndTime.HasValue ? DateTime.SpecifyKind(dto.EndTime.Value, DateTimeKind.Utc) : null,
            Description = dto.Description,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        _context.TripRequests.Add(entity);
        await _context.SaveChangesAsync();

        return new TripRequestDto
        {
            RequestId = entity.RequestId,
            CreatedAt = entity.CreatedAt,
            TripDate = entity.TripDate,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            VehicleType = entity.VehicleType,
            Status = entity.Status,
            Description = entity.Description
        };
    }
}