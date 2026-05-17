namespace CP.Server.DTO;

public class AvailableVehicleDto
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string LicensePlate { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public string PrimaryDriverName { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
}