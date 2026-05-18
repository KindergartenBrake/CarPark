namespace CP.Server.DTO;

public class DriverTripDto
{
    public int Id { get; set; }
    public DateTime ScheduledDate { get; set; }
    public string VehicleName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal? StartOdometer { get; set; }
    public decimal? EndOdometer { get; set; }
}

public class StartTripDto
{
    public decimal StartOdometer { get; set; }
}

public class EndTripDto
{
    public decimal EndOdometer { get; set; }
}