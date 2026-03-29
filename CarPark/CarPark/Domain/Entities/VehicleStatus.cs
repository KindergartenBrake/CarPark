namespace CarPark.Domain.Entities;

/// <summary>
/// Наименование статуса (на работе, в ремонте...)
/// </summary>
/// <param name="name"></param>
/// <param name="description"></param> описание статуса
public class VehicleStatus(string name, string? description)
{
    public int Id { get; set; }
    public string Name { get; set; } = name;           
    public string? Description { get; set; } = description; 

    public List<Vehicle> Vehicles { get; set; } = [];
}
