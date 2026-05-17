namespace CP.Server.DTO;

public class AssignTripDto
{
    public int VehicleId { get; set; }
    public int DriverId { get; set; }
}

public class RejectTripDto
{
    public string? Reason { get; set; }
}