namespace CP.Server.DTO;

public class CreateTripRequestDto
{
    public string VehicleType { get; set; } = string.Empty;
    public DateTime TripDate { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? Description { get; set; }
}