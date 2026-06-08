namespace CP.Server.DTO;

// DTO для отображения информации о водителе в профиле
public class DriverProfileDto
{
    // Полное имя водителя
    public string FullName { get; set; } = "";
    // Email водителя
    public string Email { get; set; } = "";
    // Телефон водителя
    public string Phone { get; set; } = "";
    // Номер водительского удостоверения
    public string DriverLicenseNumber { get; set; } = "";
    // Дата окончания водительского удостоверения
    public DateTime LicenseExpiration { get; set; }
    // Статус водителя (активен/неактивен)
    public bool IsActive { get; set; }
}
