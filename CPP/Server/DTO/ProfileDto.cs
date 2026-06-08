namespace CP.Server.DTO;

// DTO для отображения информации о профиле пользователя
public class ProfileDto
{
    // ID пользователя
    public string Id { get; set; } = "";
    // Имя пользователя

    public string UserName { get; set; } = "";
    // Email пользователя
    public string Email { get; set; } = "";
    // Номер телефона пользователя
    public string PhoneNumber { get; set; } = "";
}