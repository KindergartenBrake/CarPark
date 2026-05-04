namespace CarPark.API.DTO;
public record DriverDto(
    int Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    string LicenseNumber,
    string? Phone, string? Email,
    bool IsActive
);