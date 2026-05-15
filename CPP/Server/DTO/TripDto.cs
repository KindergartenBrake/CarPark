namespace CP.Server.DTO;

public record TripDto(
    int Id,
    int DriverId,
    int VehicleId,
    DateTime TripDate,
    DateTime StartTime,
    DateTime EndTime,
    int StartMileage,
    int EndMileage,
    string Status
);
