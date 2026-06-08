namespace CP.Server.Models.CarPark;

// Модель для заявки на поездку
public class TripRequest
{
    // ID заявки на поездку
    public int RequestId { get; set; }
    
    // Связь с Identity пользователем (ID пользователя)
    public string UserId { get; set; } = string.Empty;
    // Связь с Identity пользователем (пользователь)
    public AspNetUser? User { get; set; }
    
    // Тип транспортного средства
    public string? VehicleType { get; set; }
    
    // Дата поездки
    public DateTime TripDate { get; set; }
    // Время начала поездки
    public TimeSpan? StartTime { get; set; }
    // Время окончания поездки
    public TimeSpan? EndTime { get; set; }
    
    // Описание поездки
    public string? Description { get; set; }
    
    // Связь с транспортным средством (ID транспортного средства)
    public int? VehicleId { get; set; }
    // Связь с транспортным средством (транспортное средство)
    public Vehicle? Vehicle { get; set; }
    
    // Связь с водителем (ID водителя)
    public int? DriverId { get; set; }
    // Связь с водителем (водитель)
    public Driver? Driver { get; set; }
    
    // Дата создания заявки
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    // Статус заявки
    public string Status { get; set; } = "Pending";
    
    // Причина отклонения заявки
    public string? RejectionReason { get; set; }
    // Связь с поездкой (поездка)
    public Trip? Trip { get; set; }
}