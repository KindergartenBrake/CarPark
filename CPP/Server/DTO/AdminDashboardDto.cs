namespace CP.Server.DTO;

public class AdminDashboardDto
{
    public int PendingRequests { get; set; }
    public int ActiveTrips { get; set; }
    public int AvailableVehicles { get; set; }
    public int ActiveDrivers { get; set; }
    public int TotalVehicles { get; set; }
    public int VehiclesInRepair { get; set; }
    public int CompletedTripsToday { get; set; }
    public double AverageLoad { get; set; }
    public List<RecentRequestDto> RecentRequests { get; set; } = new();
    public List<ActivityPointDto> WeeklyActivity { get; set; } = new();
}

public class RecentRequestDto
{
    public int RequestId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime TripDate { get; set; }
    public string VehicleType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class ActivityPointDto
{
    public string Day { get; set; } = string.Empty;
    public int Value { get; set; }
}