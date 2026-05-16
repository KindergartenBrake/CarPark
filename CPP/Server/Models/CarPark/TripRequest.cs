namespace CP.Server.Models.CarPark;
/// <summary>
/// 1) Идентификатор пользователя
/// 2) Дата создания
/// 3) Описание
/// 4) Статус
/// </summary>
/// <param name="userId"></param>
/// <param name="createdAt"></param>
/// <param name="description"></param>
/// <param name="status"></param>
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
    public Trip? Trip { get; set; }
}