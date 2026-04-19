namespace CarPark.Domain.Entities;

public class User(
    string username,
    string passwordHash,
    string email,
    string role,
    bool isActive)
{
    public int Id { get; set; }
    public string Username { get; set; } = username;
    public string PasswordHash { get; set; } = passwordHash;
    public string Email { get; set; } = email;
    public string Role { get; set; } = role;
    public bool IsActive { get; set; } = isActive;

    public List<TripRequest> Requests { get; set; } = [];
}