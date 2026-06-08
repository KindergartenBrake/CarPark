namespace CP.Server.DTO;

// DTO для создания новой поездки
public class CreateTripRequestDto
{
    // Тип транспортного средства
    public string VehicleType { get; set; } = string.Empty;
    // Дата поездки
    public DateTime TripDate { get; set; }
    // Время начала поездки
    public TimeSpan? StartTime { get; set; }
    // Время окончания поездки
    public TimeSpan? EndTime { get; set; }
    // Описание поездки
    public string? Description { get; set; }
}