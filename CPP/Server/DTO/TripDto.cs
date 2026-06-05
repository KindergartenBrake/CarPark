namespace CP.Server.DTO;

public class TripDto
{
    public int TripId { get; set; }
    public int RequestId { get; set; }
    public string DriverName { get; set; } = string.Empty;
    public string VehicleName { get; set; } = string.Empty;
    public DateTime TripDate { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public decimal? StartOdometer { get; set; }
    public decimal? EndOdometer { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Route { get; set; }
    public string? Comment { get; set; }

}