using CP.Server.DTO;

namespace CP.Server.Services.Interfaces
{
    public interface IAdminVehicleService
    {
        Task<List<AdminVehicleDto>> GetAllVehiclesAsync(string? search = null);
        Task<AdminVehicleDto?> GetByIdAsync(int id);
        Task<AdminVehicleDto> CreateAsync(CreateVehicleDto dto);
        Task UpdateAsync(int id, CreateVehicleDto dto);
        Task DeleteAsync(int id);
        Task DeactivateAsync(int id);
    }
}
