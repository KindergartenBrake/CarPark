namespace CP.Server.DTO;

// DTO для отображения информации о сотруднике в панели управления
public class EmployeeDto
{
    // ID пользователя
    public string UserId { get; set; } = string.Empty;
    // Полное имя сотрудника
    public string FullName { get; set; } = string.Empty;
    // Email сотрудника
    public string Email { get; set; } = string.Empty;
    // Общее количество заявок
    public int TotalRequests { get; set; }
    // Активные заявки
    public int ActiveRequests { get; set; }
    // Статус сотрудника (активен/неактивен)
    public bool IsActive { get; set; }
    // Заявки сотрудника
    public List<EmployeeRequestDto> Requests { get; set; } = new();
}

// Заявка сотрудника
public class EmployeeRequestDto
{
    // ID заявки
    public int RequestId { get; set; }
    // Маршрут
    public string Route { get; set; } = string.Empty;
    // Статус заявки (активная/неактивная)
    public bool IsActive { get; set; }
    // Дата поездки
    public DateTime TripDate { get; set; }
    // Статус заявки (ожидает/назначена/выполнена/отклонена)
    public string Status { get; set; } = string.Empty;
}