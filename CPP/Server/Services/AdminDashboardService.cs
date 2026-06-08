using CP.Server.DTO;
using CP.Server.Data.Repositories;
using CP.Server.Services.Interfaces;

namespace CP.Server.Services;


public class AdminDashboardService : IAdminDashboardService
{
    private readonly ITripRequestRepository _tripRequestRepo;
    private readonly ITripRepository _tripRepo;
    private readonly IVehicleRepository _vehicleRepo;
    private readonly IDriverRepository _driverRepo;

    public AdminDashboardService(
        ITripRequestRepository tripRequestRepo,
        ITripRepository tripRepo,
        IVehicleRepository vehicleRepo,
        IDriverRepository driverRepo)
    {
        _tripRequestRepo = tripRequestRepo;
        _tripRepo = tripRepo;
        _vehicleRepo = vehicleRepo;
        _driverRepo = driverRepo;
    }

    public async Task<AdminDashboardDto> GetStatsAsync()
    {
        var allRequests = await _tripRequestRepo.GetAllWithDetailsAsync();
        var allTrips = await _tripRepo.GetAllAsync();
        var allVehicles = await _vehicleRepo.GetAllAsync();
        
        var today = DateTime.UtcNow.Date;
        var todayTrips = allTrips.Where(t => t.TripDate.Date == today && t.Status == "Completed");
        
        var activeDriverIds = allTrips
            .Where(t => t.Status == "Scheduled" || t.Status == "InProgress")
            .Select(t => t.DriverId)
            .Distinct()
            .ToList();

        var stats = new AdminDashboardDto
        {
            PendingRequests = allRequests.Count(r => r.Status == "Pending"),
            ActiveTrips = allTrips.Count(t => t.Status == "InProgress"),
            AvailableVehicles = allVehicles.Count(v => v.Status == "Available"),
            ActiveDrivers = activeDriverIds.Count,
            TotalVehicles = allVehicles.Count,
            VehiclesInRepair = allVehicles.Count(v => v.Status == "InRepair"),
            CompletedTripsToday = todayTrips.Count(),
            AverageLoad = allVehicles.Any() 
                ? Math.Round((double)allTrips.Count(t => t.Status == "InProgress" || t.Status == "Scheduled") / allVehicles.Count * 100, 1)
                : 0,
            RecentRequests = allRequests
                .OrderByDescending(r => r.CreatedAt)
                .Take(5)
                .Select(r => new RecentRequestDto
                {
                    RequestId = r.RequestId,
                    EmployeeName = r.User?.UserName ?? "Неизвестно",
                    TripDate = r.TripDate,
                    VehicleType = r.VehicleType ?? "Не указан",
                    Status = r.Status ?? "Pending"
                }).ToList(),
            WeeklyActivity = GetWeeklyActivity(allTrips)
        };
        
        return stats;
    }

    private List<ActivityPointDto> GetWeeklyActivity(List<Models.CarPark.Trip> allTrips)
    {
        var weekDays = new List<(string Name, int Offset)>
        {
            ("ПН", -6), ("ВТ", -5), ("СР", -4), ("ЧТ", -3), ("ПТ", -2), ("СБ", -1), ("ВС", 0)
        };
        
        return weekDays.Select(w => new ActivityPointDto
        {
            Day = w.Name,
            Value = allTrips.Count(t => t.TripDate.Date == DateTime.UtcNow.Date.AddDays(w.Offset))
        }).ToList();
    }
}