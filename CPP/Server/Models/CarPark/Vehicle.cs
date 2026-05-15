namespace CP.Server.Models.CarPark;
/// <summary>
/// 1) Госномер
/// 2) Вин-номер
/// 3) Марка
/// 4) Модель
/// 5) Год выпуска
/// 6) Пробег
/// 7) Тип топлива
/// 8) Тип тс
/// 9) Статус(состояние)
/// 10) Страховка
/// </summary>
/// <param name="licensePlate"></param>
/// <param name="vinNumber"></param>
/// <param name="brand"></param>
/// <param name="model"></param>
/// <param name="year"></param>
/// <param name="mileage"></param>
/// <param name="fuelType"></param>
/// <param name="type"></param>
/// <param name="status"></param>
/// <param name="hasInsurance"></param>
public class Vehicle(
    string licensePlate,
    string vinNumber,
    string brand,
    string model,
    int year,
    int mileage,
    string fuelType,
    string type,
    string status,
    bool hasInsurance)
{
    public int Id { get; set; }
    public string LicensePlate { get; set; } = licensePlate;
    public string VinNumber { get; set; } = vinNumber;
    public string Brand { get; set; } = brand;
    public string Model { get; set; } = model;
    public int Year { get; set; } = year;
    public int Mileage { get; set; } = mileage;
    public string FuelType { get; set; } = fuelType;
    public string Type { get; set; } = type;
    public string Status { get; set; } = status;
    public bool HasInsurance { get; set; } = hasInsurance;

    public List<Trip> Trips { get; set; } = [];
}