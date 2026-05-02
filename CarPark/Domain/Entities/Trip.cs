namespace CarPark.Domain.Entities;

public class Trip(
    int driverId,
    int vehicleId,
    int requestId,
    DateTime tripDate,
    DateTime startTime,
    DateTime endTime,
    int startMileage,
    int endMileage,
    string status)
{
    public int Id { get; set; }
    public int DriverId { get; set; } = driverId;
    public int VehicleId { get; set; } = vehicleId;
    public int RequestId { get; set; } = requestId;

    public DateTime TripDate { get; set; } = tripDate;
    public DateTime StartTime { get; set; } = startTime;
    public DateTime EndTime { get; set; } = endTime;

    public int StartMileage { get; set; } = startMileage;
    public int EndMileage { get; set; } = endMileage;

    public string Status { get; set; } = status;

    public Driver? Driver { get; set; }
    public Vehicle? Vehicle { get; set; }
    public TripRequest? Request { get; set; }
}