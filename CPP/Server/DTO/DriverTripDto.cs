namespace CP.Server.DTO;

// DTO для отображения информации о поездке водителя в профиле
public class DriverTripDto
{
    // ID поездки
    public int Id { get; set; }
    // Дата поездки
    public DateTime ScheduledDate { get; set; }
    // Название транспортного средства
    public string VehicleName { get; set; } = string.Empty;
    // Статус поездки
    public string Status { get; set; } = string.Empty;
    // Пробег в начале поездки
    public decimal? StartOdometer { get; set; }
    // Пробег в конце поездки
    public decimal? EndOdometer { get; set; }
}

// DTO для начала поездки
public class StartTripDto
{
    // Пробег в начале поездки
    public decimal StartOdometer { get; set; }
}

// DTO для окончания поездки
public class EndTripDto
{
    // Пробег в конце поездки
    public decimal EndOdometer { get; set; }
}