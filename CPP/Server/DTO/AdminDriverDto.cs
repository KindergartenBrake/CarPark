namespace CP.Server.DTO;

public class AdminDriverDto
{
    public int DriverId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public DateTime LicenseExpireDate { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
    public string? VehicleName { get; set; }
    public string? IdentityUser { get; set; }
}

public class CreateDriverDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LicenseNumber { get; set; } = string.Empty;
    public DateTime LicenseExpireDate { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; } = true;
    public int? VehicleId { get; set; }
    public string? UserId { get; set; }
}