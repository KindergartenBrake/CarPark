using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CP.Server.Models.CarPark;

// Модель для транспортного средства
public class Vehicle
{
    // ID транспортного средства
    public int VehicleId { get; set; }
    // Номер транспортного средства
    public string LicensePlate { get; set; } = string.Empty;
    // VIN транспортного средства
    public string Vin { get; set; } = string.Empty;
    // Марка транспортного средства
    public string Brand { get; set; } = string.Empty;
    // Модель транспортного средства
    public string Model { get; set; } = string.Empty;
    // Год выпуска транспортного средства
    public int Year { get; set; }
    // Пробег транспортного средства
    public decimal Mileage { get; set; }
    // Тип топлива транспортного средства
    public string? FuelType { get; set; }
    // Тип транспортного средства
    public string? VehicleType { get; set; }
    // Статус транспортного средства
    public string? Status { get; set; }
    // Страховка транспортного средства
    public string? Insurance { get; set; }
    // Дата окончания страховки транспортного средства
    public DateTime? InsuranceExpiryDate { get; set; }
    
    // Внешний ключ для основного водителя
    public int? DriverId { get; set; }
    
    // Основной водитель (один к одному)
    [ForeignKey("DriverId")]
    public virtual Driver? PrimaryDriver { get; set; }
    
    // Коллекция водителей (связь один ко многим)
    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
    
    // Заявки на поездки транспортного средства
    public virtual ICollection<TripRequest> TripRequests { get; set; } = new List<TripRequest>();
    // Поездки транспортного средства
    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}