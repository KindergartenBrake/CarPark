using CP.Server.DTO;

namespace CP.Server.Services.Interfaces
{
    public interface IAdminDriverService
    {
        Task<List<AdminDriverDto>> GetAllAsync(string? search = null);
        Task<AdminDriverDto?> GetByIdAsync(int id);
        Task<AdminDriverDto> CreateAsync(CreateDriverDto dto);
        Task UpdateAsync(int id, CreateDriverDto dto);
        Task DeactivateAsync(int id);
        Task<List<UserLookupDto>> GetAvailableUsersAsync();
        Task<List<UserLookupDto>> GetAllUsersAsync();

    }
}
