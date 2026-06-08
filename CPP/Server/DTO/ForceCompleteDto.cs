namespace CP.Server.DTO;

// DTO для принудительного завершения поездки
public class ForceCompleteDto
{
    // Комментарий к завершению поездки
    public string Comment { get; set; } = string.Empty;
}