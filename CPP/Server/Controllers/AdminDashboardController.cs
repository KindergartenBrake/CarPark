using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CP.Server.DTO;
using CP.Server.Services;

namespace CP.Server.Controllers;

[ApiController]
[Route("api/admin/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IAdminDashboardService _service;

    public DashboardController(IAdminDashboardService service)
    {
        _service = service;
    }

    [HttpGet("stats")]
    public async Task<ActionResult<AdminDashboardDto>> GetStats()
    {
        var stats = await _service.GetStatsAsync();
        return Ok(stats);
    }
}