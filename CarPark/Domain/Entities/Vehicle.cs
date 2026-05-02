namespace CarPark.Domain.Entities;

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