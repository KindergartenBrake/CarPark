namespace CP.Server.DTO;

public class EmployeeDashboardDto
{
    public int PendingRequests { get; set; }
    public int ApprovedRequests { get; set; }
    public int CompletedRequests { get; set; }
    public int RejectedRequests { get; set; }
    public UpcomingTripDto? UpcomingTrip { get; set; }
    public List<RecentRequestDto> RecentRequests { get; set; } = new();
}

public class UpcomingTripDto
{
    public int RequestId { get; set; }
    public DateTime TripDate { get; set; }
    public string VehicleName { get; set; } = string.Empty;
    public string DriverName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}


