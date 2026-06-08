namespace CP.Server.DTO;

// DTO для отображения информации о поездках сотрудника в панели управления
public class EmployeeDashboardDto
{
    // Количество заявок, ожидающих назначения
    public int PendingRequests { get; set; }
    // Количество заявок, одобренных
    public int ApprovedRequests { get; set; }
    // Количество заявок, завершенных
    public int CompletedRequests { get; set; }
    // Количество заявок, отклоненных
    public int RejectedRequests { get; set; }
    // Ближайшая поездка
    public UpcomingTripDto? UpcomingTrip { get; set; }
    // Последние заявки
    public List<RecentRequestDto> RecentRequests { get; set; } = new();
}

// Ближайшая поездка
public class UpcomingTripDto
{
    // ID заявки
    public int RequestId { get; set; }
    // Дата поездки
    public DateTime TripDate { get; set; }
    // Название транспортного средства
    public string VehicleName { get; set; } = string.Empty;
    // Имя водителя
    public string DriverName { get; set; } = string.Empty;
    // Статус заявки
    public string Status { get; set; } = string.Empty;
}


