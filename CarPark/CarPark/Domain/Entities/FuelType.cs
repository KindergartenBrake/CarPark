namespace CarPark.Domain.Entities;

/// <summary>
/// 
/// </summary>
/// <param name="name"></param> Бензин, Дизель, Газ...
public class FuelType(string name)
{
    public int Id { get; set; }
    public string Name { get; set; } = name; 
    public List<Vehicle> Vehicles { get; set; } = [];
}
