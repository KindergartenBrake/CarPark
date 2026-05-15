namespace CP.Server.DTO;

public record VehicleDto(
    int Id,
    string LicensePlate,
    string Brand,
    string Model,
    int Mileage,
    string Status
);
