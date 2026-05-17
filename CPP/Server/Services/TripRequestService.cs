using CP.Server.Data;
using CP.Server.DTO;
using Microsoft.EntityFrameworkCore;

namespace CP.Server.Services;

public interface ITripRequestService
{
    Task<List<TripRequestDto>> GetMyRequestsAsync(string userId);
    Task<TripRequestDto?> GetByIdAsync(int id);
    Task<TripRequestDto> CreateAsync(CreateTripRequestDto dto, string userId);
    Task<List<TripRequestForAdminDto>> GetAllRequestsAsync();
    Task<bool> AssignTripAsync(int requestId, int vehicleId, int driverId);
    Task<bool> RejectTripAsync(int requestId, string? reason);
    Task<List<AvailableVehicleDto>> GetAvailableVehiclesAsync();
    Task<List<AvailableDriverDto>> GetAvailableDriversAsync();
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
    public async Task<List<TripRequestForAdminDto>> GetAllRequestsAsync()
    {
        return await _context.TripRequests
            .Include(r => r.User)
            .Include(r => r.Vehicle)
            .Include(r => r.Driver)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new TripRequestForAdminDto
            {
                Id = r.RequestId,
                EmployeeName = r.User != null ? r.User.UserName ?? r.User.Email : "Неизвестный",
                EmployeeEmail = r.User != null ? r.User.Email ?? "" : "",
                CreatedAt = r.CreatedAt,
                PlannedTripDate = r.TripDate,
                VehicleType = r.VehicleType ?? "Не указан",
                Status = r.Status ?? "Pending",
                DriverName = r.Driver != null ? $"{r.Driver.LastName} {r.Driver.FirstName}" : null,
                VehicleName = r.Vehicle != null ? $"{r.Vehicle.Brand} {r.Vehicle.Model}" : null,
                VehicleBrand = r.Vehicle != null ? r.Vehicle.Brand : null,
                VehicleModel = r.Vehicle != null ? r.Vehicle.Model : null,
                LicensePlate = r.Vehicle != null ? r.Vehicle.LicensePlate : null,
                TripStarted = r.Trip != null && r.Trip.Status == "InProgress",
                TripId = r.Trip != null ? r.Trip.TripId : null,
                Description = r.Description
            })
            .ToListAsync();
    }

    public async Task<bool> AssignTripAsync(int requestId, int vehicleId, int driverId)
    {
        var request = await _context.TripRequests
            .Include(r => r.Trip)
            .FirstOrDefaultAsync(r => r.RequestId == requestId);

        if (request == null || request.Status != "Pending")
            return false;

        var vehicle = await _context.Vehicles.FindAsync(vehicleId);
        var driver = await _context.Drivers.FindAsync(driverId);

        if (vehicle == null || driver == null)
            return false;

        // Обновляем статус заявки
        request.Status = "Approved";
        request.VehicleId = vehicleId;
        request.DriverId = driverId;

        // Создаём поездку
        var trip = new Models.CarPark.Trip
        {
            RequestId = requestId,
            VehicleId = vehicleId,
            DriverId = driverId,
            TripDate = request.TripDate,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Status = "Scheduled"
        };

        _context.Trips.Add(trip);

        // Обновляем статус автомобиля (делаем его занятым)
        vehicle.Status = "InUse";

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RejectTripAsync(int requestId, string? reason)
    {
        var request = await _context.TripRequests.FindAsync(requestId);
        
        if (request == null || request.Status != "Pending")
            return false;

        request.Status = "Rejected";
        request.RejectionReason = reason;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<AvailableVehicleDto>> GetAvailableVehiclesAsync()
    {
        return await _context.Vehicles
            .Include(v => v.Driver)
            .Where(v => v.Status == "Available")
            .Select(v => new AvailableVehicleDto
            {
                Id = v.VehicleId,
                Brand = v.Brand,
                Model = v.Model,
                LicensePlate = v.LicensePlate,
                VehicleType = v.VehicleType ?? "Не указан",
                PrimaryDriverName = v.Driver != null ? $"{v.Driver.LastName} {v.Driver.FirstName}" : "Не назначен",
                IsAvailable = v.Status == "Available"
            })
            .ToListAsync();
    }

    public async Task<List<AvailableDriverDto>> GetAvailableDriversAsync()
    {
        return await _context.Drivers
            .Where(d => d.IsActive)
            .Select(d => new AvailableDriverDto
            {
                Id = d.DriverId,
                FullName = $"{d.LastName} {d.FirstName} {d.MiddleName}".Trim(),
                Phone = d.Phone,
                Email = d.Email,
                IsAvailable = true
            })
            .ToListAsync();
    }
}