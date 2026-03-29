namespace CarPark.Domain.Entities;

/// <summary>
/// 
/// </summary>
/// <param name="firstName"></param>
/// <param name="lastName"></param>
/// <param name="middleName"></param>
/// <param name="licenseNumber"></param>
/// <param name="licenseIssueDate"></param> Дата выдачи прав
/// <param name="licenseExpiryDate"></param> Дата окончания прав
/// <param name="birthDate"></param>
/// <param name="phone"></param>
/// <param name="email"></param>
/// <param name="hireDate"></param> Дата приема на работу
/// <param name="isActive"></param>
public class Driver (
    string firstName,
    string lastName,
    string? middleName,
    string licenseNumber,
    DateTime licenseIssueDate,
    DateTime licenseExpiryDate,
    DateTime birthDate,
    string? phone,
    string? email,
    DateTime hireDate,
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
    public DateTime HireDate { get; set; } = hireDate;           
    public bool IsActive { get; set; } = isActive;               
    public List<Refueling> Refuelings { get; set; } = [];
}
