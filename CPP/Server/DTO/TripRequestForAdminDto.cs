namespace CP.Server.DTO;

public class TripRequestForAdminDto
{
    public int Id { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string EmployeeEmail { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime PlannedTripDate { get; set; }
    public string VehicleType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? DriverName { get; set; }
    public string? VehicleName { get; set; }
    public string? VehicleBrand { get; set; }
    public string? VehicleModel { get; set; }
    public string? LicensePlate { get; set; }
    public bool TripStarted { get; set; }
    public int? TripId { get; set; }
    public string? Description { get; set; }
}