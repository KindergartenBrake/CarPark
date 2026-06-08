namespace CP.Server.DTO;

// DTO для смены пароля
public class ChangePasswordDto
{
    // Старый пароль
    public string OldPassword { get; set; } = "";
    // Новый пароль
    public string NewPassword { get; set; } = "";
}