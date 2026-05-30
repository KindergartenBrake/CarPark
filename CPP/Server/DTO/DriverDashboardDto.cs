namespace CP.Server.DTO;

public class DriverDashboardDto
{
    public ActiveTripDto? ActiveTrip { get; set; }
    public List<ScheduledTripDto> ScheduledTrips { get; set; } = new();
    public List<TripHistoryDto> RecentTrips { get; set; } = new();
    public int CompletedTrips { get; set; }
    public decimal TotalMileage { get; set; }
    public string TotalDriveTime { get; set; } = string.Empty;
}

public class ActiveTripDto
{
    public int TripId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string VehicleName { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public decimal StartOdometer { get; set; }
}

public class ScheduledTripDto
{
    public int TripId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string VehicleName { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
}

public class TripHistoryDto
{
    public DateTime Date { get; set; }
    public string VehicleName { get; set; } = string.Empty;
    public decimal Mileage { get; set; }
}