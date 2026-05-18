namespace CP.Server.Models.CarPark;

public class Driver
{
    public int DriverId { get; set; }
    
    // Связь с Identity пользователем
    public string? UserId { get; set; }
    public AspNetUser? User { get; set; }
    
    // Личные данные
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    
    // Водительское удостоверение
    public string LicenseNumber { get; set; } = string.Empty;
    public DateTime LicenseIssueDate { get; set; }
    public DateTime LicenseExpiryDate { get; set; }
    
    
    // Статус активности
    public bool IsActive { get; set; } = true;
    
    // Закреплённый автомобиль
    public int? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    
    // Навигационные свойства
    public ICollection<TripRequest> TripRequests { get; set; } = new List<TripRequest>();
    public ICollection<Trip> Trips { get; set; } = new List<Trip>();
}