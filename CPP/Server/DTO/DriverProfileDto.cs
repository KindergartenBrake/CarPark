namespace CP.Server.DTO;

public class DriverProfileDto
{
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string DriverLicenseNumber { get; set; } = "";
    public DateTime LicenseExpiration { get; set; }
    public bool IsActive { get; set; }
}
