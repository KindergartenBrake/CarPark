namespace CP.Server.DTO;

// Управление водителями в админ панели
// DTO для отображения водителей в таблице
public class AdminDriverDto
{
    // ID водителя
    public int DriverId { get; set; }
    // Полное имя водителя
    public string FullName { get; set; } = string.Empty;
    // Номер водительского удостоверения
    public string LicenseNumber { get; set; } = string.Empty;
    // Дата истечения водительского удостоверения
    public DateTime LicenseExpireDate { get; set; }
    // Телефон водителя
    public string? Phone { get; set; }
    // Статус водителя (активен/неактивен)
    public bool IsActive { get; set; }
    // Название транспортного средства
    public string? VehicleName { get; set; }
    // ID пользователя
    public string? IdentityUser { get; set; }
}


// DTO для создания нового водителя и редактирования существующего
public class CreateDriverDto
{
    // Имя водителя
    public string FirstName { get; set; } = string.Empty;
    // Фамилия водителя
    public string LastName { get; set; } = string.Empty;
    // Отчество водителя
    public string? MiddleName { get; set; }
    // Номер водительского удостоверения
    public string LicenseNumber { get; set; } = string.Empty;
    // Дата истечения водительского удостоверения
    public DateTime LicenseExpireDate { get; set; }
    // Телефон водителя
    public string? Phone { get; set; }
    // Статус водителя (активен/неактивен)
    public bool IsActive { get; set; } = true;
    // ID транспортного средства
    public int? VehicleId { get; set; }
    // ID пользователя
    public string? UserId { get; set; }
}