namespace CP.Server.DTO;

// DTO для отображения доступных транспортных средств в таблице назначения поездки
public class AvailableVehicleDto
{
    // ID транспортного средства
    public int Id { get; set; }
    // Марка транспортного средства
    public string Brand { get; set; } = string.Empty;
    // Модель транспортного средства
    public string Model { get; set; } = string.Empty;
    // Номер транспортного средства
    public string LicensePlate { get; set; } = string.Empty;
    // Тип транспортного средства
    public string VehicleType { get; set; } = string.Empty;
    // Имя водителя
    public string PrimaryDriverName { get; set; } = string.Empty;
    // Статус транспортного средства (доступен/недоступен)
    public bool IsAvailable { get; set; }
}