using CP.Server.DTO;

namespace CP.Server.Services.Interfaces

{
    public interface IAdminDashboardService
    {
        Task<AdminDashboardDto> GetStatsAsync();
    }
}
