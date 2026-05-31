using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CP.Server.Models.CarPark;

public class Vehicle
{
    public int VehicleId { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal Mileage { get; set; }
    public string? FuelType { get; set; }
    public string? VehicleType { get; set; }
    public string? Status { get; set; }
    public string? Insurance { get; set; }
    public DateTime? InsuranceExpiryDate { get; set; }
    
    // Внешний ключ для основного водителя
    public int? DriverId { get; set; }
    
    // Основной водитель (один к одному)
    [ForeignKey("DriverId")]
    public virtual Driver? PrimaryDriver { get; set; }
    
    // Коллекция водителей (связь один ко многим)
    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
    
    // Навигационные свойства для связанных сущностей
    public virtual ICollection<TripRequest> TripRequests { get; set; } = new List<TripRequest>();
    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}