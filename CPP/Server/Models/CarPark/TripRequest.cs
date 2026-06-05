namespace CP.Server.Models.CarPark;

public class TripRequest
{
    public int RequestId { get; set; }
    
    public string UserId { get; set; } = string.Empty;
    public AspNetUser? User { get; set; }
    
    public string? VehicleType { get; set; }
    
    public DateTime TripDate { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    
    public string? Description { get; set; }
    
    public int? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    
    public int? DriverId { get; set; }
    public Driver? Driver { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public string Status { get; set; } = "Pending";
    
    public string? RejectionReason { get; set; }
    
    public Trip? Trip { get; set; }
}