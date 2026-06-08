namespace CP.Server.DTO;

// DTO для отображения информации о транспортном средстве
public class VehicleDto
{
    // ID транспортного средства
    public int VehicleId { get; set; }
    // Марка транспортного средства
    public string Brand { get; set; } = string.Empty;
    // Модель транспортного средства
    public string Model { get; set; } = string.Empty;
    // Год выпуска
    public int Year { get; set; }
    // Номер транспортного средства
    public string LicensePlate { get; set; } = string.Empty;
    // Тип транспортного средства
    public string VehicleType { get; set; } = string.Empty;
    // Тип топлива
    public string FuelType { get; set; } = string.Empty;
    // Пробег
    public decimal Mileage { get; set; }
    // Статус транспортного средства
    public string Status { get; set; } = string.Empty;
    // Имя водителя
    public string? DriverName { get; set; }
    // Статус доступности транспортного средства
    public bool IsAvailable => Status == "Available";
    // Причина недоступности транспортного средства
    public string UnavailableReason => Status switch
    {
        "InRepair" => "В ремонте",
        "Decommissioned" => "Списан",
        _ => "Занят"
    };
}