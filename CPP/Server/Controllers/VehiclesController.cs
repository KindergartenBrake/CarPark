using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CP.Server.Services;

namespace CP.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Employee")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;
    
    public VehiclesController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CP.Server.DTO.VehicleDto>>> GetVehicles([FromQuery] string? type = null)
    {
        var vehicles = await _vehicleService.GetVehiclesAsync(type);
        return Ok(vehicles);
    }
}