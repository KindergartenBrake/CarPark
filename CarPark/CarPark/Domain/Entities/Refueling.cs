namespace CarPark.Domain.Entities;

/// <summary>
/// Total Cast - цена за бенз общий 
/// mileageAtRefueling = пробег при заправке (УДАЛИТЬ)
/// </summary>
/// <param name="vehicleId"></param>
/// <param name="driverId"></param>
/// <param name="refuelingDate"></param>
/// <param name="liters"></param>
/// <param name="pricePerLiter"></param>
/// <param name="mileageAtRefueling"></param>
/// <param name="gasStation"></param>
public class Refueling(
    int vehicleId,
    int driverId,
    DateTime refuelingDate,
    decimal liters,
    decimal pricePerLiter,
    int mileageAtRefueling,
    string? gasStation)
{
    public int Id { get; set; }
    public int VehicleId { get; set; } = vehicleId;
    public int DriverId { get; set; } = driverId;
    public DateTime RefuelingDate { get; set; } = refuelingDate;  
    public decimal Liters { get; set; } = liters;                 
    public decimal PricePerLiter { get; set; } = pricePerLiter;   
    public decimal TotalCost { get; set; }                       
    public int MileageAtRefueling { get; set; } = mileageAtRefueling;
    public string? GasStation { get; set; } = gasStation;        

    public Vehicle? Vehicle { get; set; }
    public Driver? Driver { get; set; }
}
