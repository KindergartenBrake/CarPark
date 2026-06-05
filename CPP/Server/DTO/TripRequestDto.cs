namespace CP.Server.DTO;

public class TripRequestDto
{
    public int RequestId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime TripDate { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public string? VehicleType { get; set; }
    public string Status { get; set; } = "Pending";
    public string? Description { get; set; }
    public string? RejectionReason { get; set; }
    
    // Назначенный автомобиль и водитель (если Approved)
    public string? VehicleBrand { get; set; }
    public string? VehicleModel { get; set; }
    public string? LicensePlate { get; set; }
    public string? DriverName { get; set; }
    public string? DriverPhone { get; set; }
}