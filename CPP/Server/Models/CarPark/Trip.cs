namespace CP.Server.Models.CarPark;

// Модель для поездки
public class Trip
{
    // ID поездки
    public int TripId { get; set; }
    // ID заявки на поездку
    public int RequestId { get; set; }
    // Связь с заявкой на поездку (заявка на поездку)
    public TripRequest TripRequest { get; set; } = null!;
    // ID транспортного средства
    public int VehicleId { get; set; }
    // Связь с транспортным средством (транспортное средство)
    public Vehicle Vehicle { get; set; } = null!;
    // ID водителя
    public int DriverId { get; set; }
    // Связь с водителем (водитель)
    public Driver Driver { get; set; } = null!;
    // Дата поездки
    public DateTime TripDate { get; set; }
    // Время начала поездки
    public DateTime? StartTime { get; set; }
    // Время окончания поездки
    public DateTime? EndTime { get; set; }
    // Пробег в начале поездки
    public decimal? StartOdometer { get; set; }
    // Пробег в конце поездки
    public decimal? EndOdometer { get; set; }
    // Расстояние поездки (рассчитывается автоматически) 
    public decimal? Distance => EndOdometer.HasValue && StartOdometer.HasValue 
        ? EndOdometer.Value - StartOdometer.Value 
        : null;
    // Статус поездки
    public string Status { get; set; } = "Scheduled";
}