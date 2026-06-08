namespace CP.Server.DTO;

// DTO для отображения информации о поездке
public class TripDto
{
    // ID поездки
    public int TripId { get; set; }
    // ID заявки
    public int RequestId { get; set; }
    // Имя водителя
    public string DriverName { get; set; } = string.Empty;
    // Название транспортного средства
    public string VehicleName { get; set; } = string.Empty;
    // Дата поездки
    public DateTime TripDate { get; set; }
    // Время начала поездки
    public TimeSpan? StartTime { get; set; }
    // Время окончания поездки
    public TimeSpan? EndTime { get; set; }
    // Пробег в начале поездки
    public decimal? StartOdometer { get; set; }
    // Пробег в конце поездки
    public decimal? EndOdometer { get; set; }
    // Статус поездки
    public string Status { get; set; } = string.Empty;
    // Маршрут поездки
    public string? Route { get; set; }
    // Комментарий к поездке
    public string? Comment { get; set; }

}