namespace CP.Server.DTO;

// Управление транспортными средствами в админ панели
// DTO для отображения транспортных средств в таблице
public class AdminVehicleDto
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
    public DateTime? InsuranceExpire { get; set; }
    // Имя водителя
    public string? DriverName { get; set; }
    // ID водителя
    public int? DriverId { get; set; }
    // Есть активные поездки
    public bool HasActiveTrips { get; set; }
    // Поездки
    public List<AdminTripDto> Trips { get; set; } = new();
}

// Поездки отображение в таблице транспортного средства
public class AdminTripDto
{
    // ID поездки
    public int TripId { get; set; }
    // Маршрут
    public string Route { get; set; } = string.Empty;
    // Расстояние
    public decimal Distance { get; set; }
}

// DTO для создания нового транспортного средства и редактирования существующего
public class CreateVehicleDto
{
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
    public string Status { get; set; } = "Available";
    // Дата истечения страховки
    public DateTime? InsuranceExpire { get; set; }
    // ID водителя
    public int? DriverId { get; set; }
}