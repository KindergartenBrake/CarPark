namespace CP.Server.Models.CarPark;

// Модель для водителя
public class Driver
{
    // ID водителя
    public int DriverId { get; set; }
    
    // Связь с Identity пользователем (ID пользователя)
    public string? UserId { get; set; }
    // Связь с Identity пользователем (пользователь)
    public AspNetUser? User { get; set; }
    
    // Имя водителя
    public string FirstName { get; set; } = string.Empty;
    // Фамилия водителя
    public string LastName { get; set; } = string.Empty;
    // Отчество водителя
    public string? MiddleName { get; set; }
    // Дата рождения водителя
    public DateTime BirthDate { get; set; }
    // Телефон водителя
    public string? Phone { get; set; }
    // Email водителя
    public string? Email { get; set; }
    
    // Номер водительского удостоверения    
    public string LicenseNumber { get; set; } = string.Empty;
    // Дата выдачи водительского удостоверения
    public DateTime LicenseIssueDate { get; set; }
    // Дата окончания срока действия водительского удостоверения
    public DateTime LicenseExpiryDate { get; set; }
    // Статус активности водителя
    public bool IsActive { get; set; } = true;
    // Связь с транспортным средством (ID транспортного средства)
    public int? VehicleId { get; set; }
    // Связь с транспортным средством (транспортное средство)
    public Vehicle? Vehicle { get; set; }
    // Заявки на поездки водителя
    public ICollection<TripRequest> TripRequests { get; set; } = new List<TripRequest>();
    // Поездки водителя
    public ICollection<Trip> Trips { get; set; } = new List<Trip>();
}