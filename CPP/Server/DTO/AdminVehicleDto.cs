namespace CP.Server.DTO;

public class AdminVehicleDto
{
    public int VehicleId { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public string FuelType { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public decimal Mileage { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? InsuranceExpire { get; set; }
    public string? DriverName { get; set; }
    public int? DriverId { get; set; }
    public bool HasActiveTrips { get; set; }
    public List<AdminTripDto> Trips { get; set; } = new();
}

public class AdminTripDto
{
    public int TripId { get; set; }
    public string Route { get; set; } = string.Empty;
    public decimal Distance { get; set; }
}

public class CreateVehicleDto
{
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public string FuelType { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public decimal Mileage { get; set; }
    public string Status { get; set; } = "Available";
    public DateTime? InsuranceExpire { get; set; }
    public int? DriverId { get; set; }
}