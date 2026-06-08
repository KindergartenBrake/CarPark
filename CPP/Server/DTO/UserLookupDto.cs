namespace CP.Server.DTO;

// DTO для отображения информации о пользователе в lookup
public class UserLookupDto
{
    // ID пользователя
    public string Id { get; set; } = string.Empty;
    // Имя пользователя
    public string UserName { get; set; } = string.Empty;
    // Email пользователя
    public string Email { get; set; } = string.Empty;
    // Роль пользователя
    public string Role { get; set; } = string.Empty;
}