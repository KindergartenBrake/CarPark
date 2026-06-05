namespace CP.Server.DTO;

public class CreateTripRequestDto
{
    public string VehicleType { get; set; } = string.Empty;
    public DateTime TripDate { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public string? Description { get; set; }
}