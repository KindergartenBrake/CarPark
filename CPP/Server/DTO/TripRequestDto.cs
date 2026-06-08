namespace CP.Server.DTO;
// DTO для отображения информации о заявке на поездку
public class TripRequestDto
{
    // ID заявки
    public int RequestId { get; set; }
    // Дата создания заявки
    public DateTime CreatedAt { get; set; }
    // Дата поездки
    public DateTime TripDate { get; set; }
    // Время начала поездки
    public TimeSpan? StartTime { get; set; }
    // Время окончания поездки
    public TimeSpan? EndTime { get; set; }
    // Тип транспортного средства
    public string? VehicleType { get; set; }
    // Статус заявки
    public string Status { get; set; } = "Pending";
    // Описание поездки
    public string? Description { get; set; }
    // Причина отклонения заявки
    public string? RejectionReason { get; set; }
    
    // Назначенный автомобиль и водитель (если Approved)
    public string? VehicleBrand { get; set; }
    // Модель транспортного средства
    public string? VehicleModel { get; set; }
    // Номер транспортного средства
    public string? LicensePlate { get; set; }
    // Имя водителя
    public string? DriverName { get; set; }
    // Номер телефона водителя
    public string? DriverPhone { get; set; }
}