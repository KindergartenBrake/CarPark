namespace CP.Server.Models.CarPark;

public class Trip
{
    public int TripId { get; set; }
    public int RequestId { get; set; }
    public TripRequest TripRequest { get; set; } = null!;
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;
    public int DriverId { get; set; }
    public Driver Driver { get; set; } = null!;
    public DateTime TripDate { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public decimal? StartOdometer { get; set; }
    public decimal? EndOdometer { get; set; }
    
    public decimal? Distance => EndOdometer.HasValue && StartOdometer.HasValue 
        ? EndOdometer.Value - StartOdometer.Value 
        : null;
    
    public string Status { get; set; } = "Scheduled";
}