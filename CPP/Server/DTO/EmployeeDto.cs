namespace CP.Server.DTO;

public class EmployeeDto
{
    public string UserId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int TotalRequests { get; set; }
    public int ActiveRequests { get; set; }
    public bool IsActive { get; set; }
    public List<EmployeeRequestDto> Requests { get; set; } = new();
}

public class EmployeeRequestDto
{
    public int RequestId { get; set; }
    public string Route { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime TripDate { get; set; }
    public string Status { get; set; } = string.Empty;
}