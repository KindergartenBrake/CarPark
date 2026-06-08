namespace CP.Server.DTO;

// DTO для отображения информации о заявке на поездку в административном панеле
public class TripRequestForAdminDto
{
    // ID заявки
    public int Id { get; set; }
    // Имя сотрудника
    public string EmployeeName { get; set; } = string.Empty;
    // Email сотрудника
    public string EmployeeEmail { get; set; } = string.Empty;
    // Дата создания заявки
    public DateTime CreatedAt { get; set; }
    // Дата поездки
    public DateTime PlannedTripDate { get; set; }
    // Тип транспортного средства
    public string VehicleType { get; set; } = string.Empty;
    // Статус заявки
    public string Status { get; set; } = string.Empty;
    // Имя водителя
    public string? DriverName { get; set; }
    // Название транспортного средства
    public string? VehicleName { get; set; }
    // Марка транспортного средства
    public string? VehicleBrand { get; set; }
    // Модель транспортного средства
    public string? VehicleModel { get; set; }
    // Номер транспортного средства
    public string? LicensePlate { get; set; }
    // Статус поездки
    public bool TripStarted { get; set; }
    // ID поездки
    public int? TripId { get; set; }
    // Описание поездки
    public string? Description { get; set; }
}