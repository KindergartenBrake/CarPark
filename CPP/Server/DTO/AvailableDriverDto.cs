namespace CP.Server.DTO;

// DTO для отображения доступных водителей в таблице назначения поездки
public class AvailableDriverDto
{
    // ID водителя
    public int Id { get; set; }
    // Полное имя водителя
    public string FullName { get; set; } = string.Empty;
    // Телефон водителя
    public string? Phone { get; set; }
    // Email водителя
    public string? Email { get; set; }
    // Статус водителя (доступен/недоступен)
    public bool IsAvailable { get; set; }
}