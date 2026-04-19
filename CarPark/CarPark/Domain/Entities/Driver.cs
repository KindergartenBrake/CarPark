namespace CarPark.Domain.Entities;

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