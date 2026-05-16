namespace CP.Server.Models.CarPark;

public class Vehicle
{
    public int VehicleId { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public string? FuelType { get; set; }
    public decimal Mileage { get; set; }
    public string Status { get; set; } = "Available";
    public string? Insurance { get; set; }
    public DateTime? InsuranceExpiryDate { get; set; }
    public int? DriverId { get; set; }
    public Driver? Driver { get; set; }
    public ICollection<TripRequest> TripRequests { get; set; } = new List<TripRequest>();
    public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    public ICollection<Driver> Drivers { get; set; } = new List<Driver>();
}