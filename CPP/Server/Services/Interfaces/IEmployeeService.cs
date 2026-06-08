using CP.Server.DTO;

namespace CP.Server.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllEmployeesAsync(string? search = null);
        Task<EmployeeDto?> GetEmployeeByIdAsync(string userId);
        Task DeactivateEmployeeAsync(string userId);

        Task ActivateAsync(string userId);
    }
}
