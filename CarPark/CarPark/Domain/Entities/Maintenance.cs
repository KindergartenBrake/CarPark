namespace CarPark.Domain.Entities;

/// <summary>
/// Техобслуживание
/// </summary>
/// <param name="vehicleId"></param> Индентификатор ID
/// <param name="maintenanceTypeId"></param> Идентификатор Вида ТО
/// <param name="plannedDate"></param> Плановая дата
/// <param name="serviceCenter"></param> 
/// <param name="workDescription"></param>
public class Maintenance(
    int vehicleId,
    int maintenanceTypeId,
    DateTime plannedDate,
    string? serviceCenter,
    string? workDescription)
{
    public int Id { get; set; }
    public int VehicleId { get; set; } = vehicleId;
    public int MaintenanceTypeId { get; set; } = maintenanceTypeId;
    public DateTime PlannedDate { get; set; } = plannedDate;     
    public DateTime? ActualDate { get; set; }                   
    public int? MileageAtService { get; set; }                  
    public decimal? Cost { get; set; }                          
    public string? ServiceCenter { get; set; } = serviceCenter;  
    public string? InvoiceNumber { get; set; }                   
    public string? WorkDescription { get; set; } = workDescription; 

   
    public Vehicle? Vehicle { get; set; }
    public MaintenanceType? MaintenanceType { get; set; }
}
