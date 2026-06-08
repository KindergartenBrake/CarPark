namespace CP.Server.DTO;

// DTO для отображения информации о водителе в панели управления
public record DriverDto(
    // ID водителя
    int Id,
    // Имя водителя
    string FirstName,
    // Фамилия водителя
    string LastName,
    // Отчество водителя
    string? MiddleName,
    // Номер водительского удостоверения
    string LicenseNumber,
    // Телефон водителя 
    string? Phone,
    // Email водителя
    string? Email,
    // Статус водителя (активен/неактивен)
    bool IsActive
);