namespace CP.Server.DTO;

// DTO для отклонения заявки на поездку
public class RejectTripRequestDto
{
    // Причина отклонения
    public string? Reason { get; set; }
}