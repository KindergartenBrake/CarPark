namespace CarPark.Domain.Entities;

/// <summary>
/// Страховки
/// </summary>
/// <param name="vehicleId"></param>
/// <param name="policyNumber"></param>
/// <param name="insuranceCompany"></param>
/// <param name="insuranceType"></param> ОСАГО, КАСКО
/// <param name="startDate"></param>
/// <param name="endDate"></param>
public class Insurance(
    int vehicleId,
    string policyNumber,
    string insuranceCompany,
    string insuranceType,
    DateTime startDate,
    DateTime endDate)
{
    public int Id { get; set; }
    public int VehicleId { get; set; } = vehicleId;
    public string PolicyNumber { get; set; } = policyNumber;     
    public string InsuranceCompany { get; set; } = insuranceCompany; 
    public string InsuranceType { get; set; } = insuranceType;   
    public DateTime StartDate { get; set; } = startDate;         
    public DateTime EndDate { get; set; } = endDate;             

    public Vehicle? Vehicle { get; set; }
}
