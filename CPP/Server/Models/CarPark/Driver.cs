namespace CP.Server.Models.CarPark;
/// <summary>
/// 1) Имя
/// 2) Фамилия
/// 3) Отчество
/// 4) Номер прав
/// 5) Дата окончания прав
/// 6) Дата выдачи прав
/// 7) День рождение
/// 8) Телефон
/// 9) Почта
/// 10) Состояние (Готовность водителя)
/// </summary>
/// <param name="firstName"></param>
/// <param name="lastName"></param>
/// <param name="middleName"></param>
/// <param name="licenseNumber"></param>
/// <param name="licenseIssueDate"></param>
/// <param name="licenseExpiryDate"></param>
/// <param name="birthDate"></param>
/// <param name="phone"></param>
/// <param name="email"></param>
/// <param name="isActive"></param>
public class Driver(
    string firstName,
    string lastName,
    string? middleName,
    string licenseNumber,
    DateTime licenseIssueDate,
    DateTime licenseExpiryDate,
    DateTime birthDate,
    string? phone,
    string? email,
    bool isActive)
{
    public int Id { get; set; }
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string? MiddleName { get; set; } = middleName;
    public string LicenseNumber { get; set; } = licenseNumber;
    public DateTime LicenseIssueDate { get; set; } = licenseIssueDate;
    public DateTime LicenseExpiryDate { get; set; } = licenseExpiryDate;
    public DateTime BirthDate { get; set; } = birthDate;
    public string? Phone { get; set; } = phone;
    public string? Email { get; set; } = email;
    public bool IsActive { get; set; } = isActive;

    public List<Trip> Trips { get; set; } = [];
}