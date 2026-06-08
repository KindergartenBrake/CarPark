namespace CP.Server.DTO;

// DTO для отображения информации о поездках водителя в панели управления
public class DriverDashboardDto
{
    // Активная поездка
    public ActiveTripDto? ActiveTrip { get; set; }
    // Запланированные поездки
    public List<ScheduledTripDto> ScheduledTrips { get; set; } = new();
    // Последние поездки
    public List<TripHistoryDto> RecentTrips { get; set; } = new();
    // Количество выполненных поездок
    public int CompletedTrips { get; set; }
    // Общий пробег
    public decimal TotalMileage { get; set; }
    // Общее время вождения
    public string TotalDriveTime { get; set; } = string.Empty;
}

// Активная поездка
public class ActiveTripDto
{
    // ID поездки
    public int TripId { get; set; }
    // Описание поездки
    public string Description { get; set; } = string.Empty;
    // Название транспортного средства
    public string VehicleName { get; set; } = string.Empty;
    // Время начала поездки
    public DateTime StartTime { get; set; }
    // Пробег в начале поездки
    public decimal StartOdometer { get; set; }
}

// Запланированная поездка
public class ScheduledTripDto
{
    // ID поездки
    public int TripId { get; set; }
    // Описание поездки
    public string Description { get; set; } = string.Empty;
    // Название транспортного средства
    public string VehicleName { get; set; } = string.Empty;
    // Дата поездки
    public DateTime ScheduledDate { get; set; }
}

// История поездок
public class TripHistoryDto
{
    // Дата поездки
    public DateTime Date { get; set; }
    // Название транспортного средства
    public string VehicleName { get; set; } = string.Empty;
    // Пробег
    public decimal Mileage { get; set; }
}