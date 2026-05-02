namespace CarPark.API.DTO;
public record TripRequestDto(
    int Id,
    int UserId,
    DateTime CreatedAt,
    string Description,
    string Status
);