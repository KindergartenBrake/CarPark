namespace CP.Server.DTO;

// DTO для отображения информации о транспортном средстве в профиле водителя
public class DriverVehicleDto
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
    // VIN транспортного средства
    public string Vin { get; set; } = string.Empty;
    // Тип топлива
    public string FuelType { get; set; } = string.Empty;
    // Тип транспортного средства
    public string VehicleType { get; set; } = string.Empty;
    // Пробег
    public decimal Mileage { get; set; }
    // Статус транспортного средства
    public string Status { get; set; } = string.Empty;
    // Дата истечения страховки
    public DateTime? InsuranceExpiration { get; set; }
    // URL изображения транспортного средства
    public string? ImageUrl { get; set; }
}