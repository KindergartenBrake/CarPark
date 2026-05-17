namespace CP.Server.DTO;

public class AvailableDriverDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public bool IsAvailable { get; set; }
}