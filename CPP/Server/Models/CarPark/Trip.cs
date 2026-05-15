namespace CP.Server.Models.CarPark;
/// <summary>
/// 1) Идентификатор водителя(поездки?)
/// 2) Идентификатор автомобиля
/// 3) Идентификатор заявки
/// 4) Дата поездки
/// 5) Время начала
/// 6) Время конца поездки
/// 7) Пробег в начале 
/// 8) Пробег в конце
/// 9) Статус
/// </summary>
/// <param name="driverId"></param>
/// <param name="vehicleId"></param>
/// <param name="requestId"></param>
/// <param name="tripDate"></param>
/// <param name="startTime"></param>
/// <param name="endTime"></param>
/// <param name="startMileage"></param>
/// <param name="endMileage"></param>
/// <param name="status"></param>
public class Trip(
    int driverId,
    int vehicleId,
    int requestId,
    DateTime tripDate,
    DateTime startTime,
    DateTime endTime,
    int startMileage,
    int endMileage,
    string status)
{
    public int Id { get; set; }
    public int DriverId { get; set; } = driverId;
    public int VehicleId { get; set; } = vehicleId;
    public int RequestId { get; set; } = requestId;

    public DateTime TripDate { get; set; } = tripDate;
    public DateTime StartTime { get; set; } = startTime;
    public DateTime EndTime { get; set; } = endTime;

    public int StartMileage { get; set; } = startMileage;
    public int EndMileage { get; set; } = endMileage;

    public string Status { get; set; } = status;

    public Driver? Driver { get; set; }
    public Vehicle? Vehicle { get; set; }
    public TripRequest? Request { get; set; }
}