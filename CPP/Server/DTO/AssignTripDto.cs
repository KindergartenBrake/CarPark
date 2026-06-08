namespace CP.Server.DTO;

// DTO для назначения поездки
public class AssignTripDto
{
    // ID транспортного средства
    public int VehicleId { get; set; }
    // ID водителя
    public int DriverId { get; set; }
}