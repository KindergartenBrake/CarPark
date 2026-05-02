namespace CarPark.API.DTO;
public record DriverDto(
    int Id,
    string FirstName,
    string LastName,
    string LicenseNumber,
    bool IsActive
);