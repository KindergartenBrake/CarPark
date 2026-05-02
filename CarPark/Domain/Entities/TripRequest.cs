namespace CarPark.Domain.Entities;

public class TripRequest(
    int userId,
    DateTime createdAt,
    string description,
    string status)
{
    public int Id { get; set; }
    public int UserId { get; set; } = userId;
    public DateTime CreatedAt { get; set; } = createdAt;
    public string Description { get; set; } = description;
    public string Status { get; set; } = status;

    public User? User { get; set; }
    public Trip? Trip { get; set; }
}