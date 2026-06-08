namespace CP.Server.DTO;

// Admin Dashboard DTO
public class AdminDashboardDto
{
    // Количество заявок, ожидающих назначения
    public int PendingRequests { get; set; }
    // Количество активных поездок
    public int ActiveTrips { get; set; }
    // Количество доступных транспортных средств
    public int AvailableVehicles { get; set; }
    // Количество активных водителей
    public int ActiveDrivers { get; set; }
    // Общее количество транспортных средств
    public int TotalVehicles { get; set; }
    // Количество транспортных средств, находящихся в ремонте
    public int VehiclesInRepair { get; set; }
    // Количество выполненных поездок за сегодня
    public int CompletedTripsToday { get; set; }
    // Средняя загрузка транспортных средств
    public double AverageLoad { get; set; }
    // Последние заявки (5 последних заявок)
    public List<RecentRequestDto> RecentRequests { get; set; } = new();
    // Еженедельная активность (7 точек для отображения активности за неделю)
    public List<ActivityPointDto> WeeklyActivity { get; set; } = new();
}

// Последние заявки отображение в таблице дашборда
public class RecentRequestDto
{
    // ID заявки
    public int RequestId { get; set; }
    // Имя сотрудника
    public string EmployeeName { get; set; } = string.Empty;
    // Дата поездки
    public DateTime TripDate { get; set; }
    // Тип транспортного средства
    public string VehicleType { get; set; } = string.Empty;
    // Статус заявки (Pending, Approved, Rejected)
    public string Status { get; set; } = string.Empty;
    // Описание поездки (цель)
    public string Description { get; set; } = string.Empty;
}

// Еженедельная активность
public class ActivityPointDto
{
    // День недели (пн, вт, ср, чт, пт, сб, вс)
    public string Day { get; set; } = string.Empty;
    // Количество поездок за день
    public int Value { get; set; }
}