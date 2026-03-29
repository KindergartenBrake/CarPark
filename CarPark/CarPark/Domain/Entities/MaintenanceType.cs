namespace CarPark.Domain.Entities;


/// <summary>
/// ТО (ТО-1, ТО-2, Замена масла...)
/// </summary>
/// <param name="name"></param> ТО-1, ТО-2, Замена масла..
/// <param name="recommendedIntervalKm"></param> Интервал замены
/// <param name="description"></param> Описание ТО
public class MaintenanceType(
    string name,
    int? recommendedIntervalKm,
    string? description)
{
    public int Id { get; set; }
    public string Name { get; set; } = name;                   
    public int? RecommendedIntervalKm { get; set; } = recommendedIntervalKm; 
    public string? Description { get; set; } = description;      

    public List<Maintenance> Maintenances { get; set; } = [];
}
