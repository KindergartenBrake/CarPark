namespace CP.Server.DTO;

public class VehicleDto
{
    public int VehicleId { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public string FuelType { get; set; } = string.Empty;
    public decimal Mileage { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? DriverName { get; set; }
    
    public bool IsAvailable => Status == "Available";
    public string UnavailableReason => Status switch
    {
        "InRepair" => "В ремонте",
        "Decommissioned" => "Списан",
        _ => "Занят"
    };
}