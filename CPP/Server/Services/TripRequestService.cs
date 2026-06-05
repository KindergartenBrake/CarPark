using CP.Server.DTO;
using CP.Server.Data.Repositories;
using CP.Server.Models.CarPark;

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
    Task<EmployeeDashboardDto> GetEmployeeDashboardAsync(string userId);

}

public class TripRequestService : ITripRequestService
{
    private readonly ITripRequestRepository _tripRequestRepo;
    private readonly IVehicleRepository _vehicleRepo;
    private readonly IDriverRepository _driverRepo;
    private readonly ITripRepository _tripRepo;

    public TripRequestService(
        ITripRequestRepository tripRequestRepo,
        IVehicleRepository vehicleRepo,
        IDriverRepository driverRepo,
        ITripRepository tripRepo)
    {
        _tripRequestRepo = tripRequestRepo;
        _vehicleRepo = vehicleRepo;
        _driverRepo = driverRepo;
        _tripRepo = tripRepo;
    }

    public async Task<List<TripRequestDto>> GetMyRequestsAsync(string userId)
    {
        var requests = await _tripRequestRepo.GetByUserIdAsync(userId);

        return requests.Select(r => new TripRequestDto
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
            VehicleBrand = r.Vehicle?.Brand,
            VehicleModel = r.Vehicle?.Model,
            LicensePlate = r.Vehicle?.LicensePlate,
            DriverName = r.Driver != null ? $"{r.Driver.LastName} {r.Driver.FirstName}" : null,
            DriverPhone = r.Driver?.Phone
        }).ToList();
    }

    public async Task<TripRequestDto?> GetByIdAsync(int id)
    {
        var r = await _tripRequestRepo.GetByIdWithDetailsAsync(id);
        if (r == null) return null;

        return new TripRequestDto
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
            VehicleBrand = r.Vehicle?.Brand,
            VehicleModel = r.Vehicle?.Model,
            LicensePlate = r.Vehicle?.LicensePlate,
            DriverName = r.Driver != null ? $"{r.Driver.LastName} {r.Driver.FirstName}" : null,
            DriverPhone = r.Driver?.Phone
        };
    }

    public async Task<TripRequestDto> CreateAsync(CreateTripRequestDto dto, string userId)
    {
        if (string.IsNullOrWhiteSpace(dto.VehicleType))
            throw new ArgumentException("Тип автомобиля обязателен");
        
        if (dto.TripDate == default)
            throw new ArgumentException("Дата поездки обязательна");
        
        if (dto.TripDate < DateTime.UtcNow)
            throw new ArgumentException("Дата поездки не может быть в прошлом");

        var entity = new TripRequest
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

        await _tripRequestRepo.AddAsync(entity);
        await _tripRequestRepo.SaveChangesAsync();

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
        var requests = await _tripRequestRepo.GetAllWithDetailsAsync();

        return requests.Select(r => new TripRequestForAdminDto
        {
            Id = r.RequestId,
            EmployeeName = r.User != null ? r.User.UserName ?? r.User.Email : "Неизвестный",
            EmployeeEmail = r.User?.Email ?? "",
            CreatedAt = r.CreatedAt,
            PlannedTripDate = r.TripDate,
            VehicleType = r.VehicleType ?? "Не указан",
            Status = r.Status ?? "Pending",
            DriverName = r.Driver != null ? $"{r.Driver.LastName} {r.Driver.FirstName}" : null,
            VehicleName = r.Vehicle != null ? $"{r.Vehicle.Brand} {r.Vehicle.Model}" : null,
            VehicleBrand = r.Vehicle?.Brand,
            VehicleModel = r.Vehicle?.Model,
            LicensePlate = r.Vehicle?.LicensePlate,
            TripStarted = r.Trip != null && r.Trip.Status == "InProgress",
            TripId = r.Trip?.TripId,
            Description = r.Description
        }).ToList();
    }

    public async Task<bool> AssignTripAsync(int requestId, int vehicleId, int driverId)
    {
        var request = await _tripRequestRepo.GetByIdAsync(requestId);
        if (request == null) return false;

        var vehicle = await _vehicleRepo.GetByIdAsync(vehicleId);
        var driver = await _driverRepo.GetByIdAsync(driverId);

        if (vehicle == null || driver == null) return false;
        
        // Проверка, что машина доступна
        if (vehicle.Status != "Available") return false;
        
        // Проверка, что водитель активен и не занят в другой поездке
        // var activeTrip = await _tripRepo.GetActiveTripByDriverId(driverId);
        // if (activeTrip != null) return false;

        // Освобождаем старую машину, если была назначена
        if (request.VehicleId.HasValue)
        {
            var oldVehicle = await _vehicleRepo.GetByIdAsync(request.VehicleId.Value);
            if (oldVehicle != null) oldVehicle.Status = "Available";
        }

        request.VehicleId = vehicleId;
        request.DriverId = driverId;
        request.Status = "Approved";
        
        vehicle.Status = "InUse"; // или "Busy"

        // Обновляем или создаём Trip
        var trip = await _tripRepo.GetByRequestIdAsync(requestId);
        if (trip == null)
        {
            trip = new Trip
            {
                RequestId = requestId,
                VehicleId = vehicleId,
                DriverId = driverId,
                TripDate = request.TripDate,
                Status = "Scheduled"
            };
            await _tripRepo.AddAsync(trip);
        }
        else
        {
            trip.VehicleId = vehicleId;
            trip.DriverId = driverId;
        }

        await _tripRequestRepo.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RejectTripAsync(int requestId, string? reason)
    {
        var request = await _tripRequestRepo.GetByIdAsync(requestId);
        if (request == null || request.Status != "Pending")
            return false;

        request.Status = "Rejected";
        request.RejectionReason = reason;
        await _tripRequestRepo.SaveChangesAsync();
        return true;
    }

    public async Task<List<AvailableVehicleDto>> GetAvailableVehiclesAsync()
    {
        var vehicles = await _vehicleRepo.GetAvailableWithDriverAsync();

        return vehicles.Select(v => new AvailableVehicleDto
        {
            Id = v.VehicleId,
            Brand = v.Brand,
            Model = v.Model,
            LicensePlate = v.LicensePlate,
            VehicleType = v.VehicleType ?? "Не указан",
            PrimaryDriverName = v.PrimaryDriver != null ? $"{v.PrimaryDriver.LastName} {v.PrimaryDriver.FirstName}" : "Не назначен",
            IsAvailable = v.Status == "Available"
        }).ToList();
    }

    public async Task<List<AvailableDriverDto>> GetAvailableDriversAsync()
    {
        var drivers = await _driverRepo.GetActiveDriversAsync();

        return drivers.Select(d => new AvailableDriverDto
        {
            Id = d.DriverId,
            FullName = $"{d.LastName} {d.FirstName} {d.MiddleName}".Trim(),
            Phone = d.Phone,
            Email = d.Email,
            IsAvailable = true
        }).ToList();
    }

    public async Task<EmployeeDashboardDto> GetEmployeeDashboardAsync(string userId)
{
    var requests = await _tripRequestRepo.GetByUserIdAsync(userId);
    
    var upcomingTrip = requests
        .Where(r => r.Status == "Approved" && r.TripDate >= DateTime.UtcNow)
        .OrderBy(r => r.TripDate)
        .Select(r => new UpcomingTripDto
        {
            RequestId = r.RequestId,
            TripDate = r.TripDate,
            VehicleName = r.Vehicle != null ? $"{r.Vehicle.Brand} {r.Vehicle.Model}" : r.VehicleType ?? "—",
            DriverName = r.Driver != null ? $"{r.Driver.LastName} {r.Driver.FirstName}" : "Не назначен",
            Status = r.Status
        })
        .FirstOrDefault();
    
    return new EmployeeDashboardDto
    {
        PendingRequests = requests.Count(r => r.Status == "Pending"),
        ApprovedRequests = requests.Count(r => r.Status == "Approved"),
        CompletedRequests = requests.Count(r => r.Status == "Completed"),
        RejectedRequests = requests.Count(r => r.Status == "Rejected"),
        UpcomingTrip = upcomingTrip,
        RecentRequests = requests
            .OrderByDescending(r => r.CreatedAt)
            .Take(5)
            .Select(r => new RecentRequestDto
            {
                RequestId = r.RequestId,
                TripDate = r.TripDate,
                VehicleType = r.VehicleType ?? "—",
                Status = r.Status ?? "Pending"
            }).ToList()
    };
}
}