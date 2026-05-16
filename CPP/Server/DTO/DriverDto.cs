namespace CP.Server.DTO;

public record DriverDto(
    int Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    string LicenseNumber,
    string? Phone, string? Email,
    bool IsActive
);