using Microsoft.VisualBasic.FileIO;

namespace CarPark.Domain.Entities;

/// <summary>
/// идентификатор топлива эту сущность сделать атрибутом 
/// в атрибут добавить тип тс
/// </summary>
/// <param name="licensePlate"></param> Государственный номер
/// <param name="vinNumber"></param> Вин-номер
/// <param name="brand"></param> Марка
/// <param name="model"></param> Модель
/// <param name="year"></param> Год выпуска
/// <param name="mileage"></param> Пробег
/// <param name="purchaseDate"></param> Дата покупки
/// <param name="purchasePrice"></param> Стоимость покупки
/// <param name="fuelTypeId"></param> Идентификатор_топлива
/// <param name="statusId"></param> Идентификатор_статуса
public class Vehicle(
    string licensePlate, 
    string vinNumber, 
    string brand,
    string model, 
    int year,
    int mileage,
    DateTime purchaseDate,
    decimal purchasePrice,
    int fuelTypeId,
    int statusId)
{
    public int Id { get; set; }
    public string LicensePlate { get; set; } = licensePlate;     
    public string VinNumber { get; set; } = vinNumber;           
    public string Brand { get; set; } = brand;                  
    public string Model { get; set; } = model;                   
    public int Year { get; set; } = year;                        
    public int Mileage { get; set; } = mileage;                  
    public DateTime PurchaseDate { get; set; } = purchaseDate;   
    public decimal PurchasePrice { get; set; } = purchasePrice;  
    public int FuelTypeId { get; set; } = fuelTypeId;          
    public int StatusId { get; set; } = statusId;            

    public FuelType? FuelType { get; set; }
    public VehicleStatus? Status { get; set; }
    public List<Maintenance> Maintenances { get; set; } = [];
    public List<Refueling> Refuelings { get; set; } = [];
    public List<Insurance> Insurances { get; set; } = [];
}
