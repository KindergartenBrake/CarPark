using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CP.Server.DTO;
using CP.Server.Services.Interfaces;

namespace CP.Server.Controllers;

[ApiController]
[Route("api/driver/trips")]
[Authorize]
public class DriverTripsController : ControllerBase
{
    private readonly IDriverTripService _service;

    public DriverTripsController(IDriverTripService service)
    {
        _service = service;
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyTrips()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var trips = await _service.GetDriverTripsAsync(userId);

        return Ok(trips);
    }

    [HttpPut("{id}/start")]
    public async Task<IActionResult> StartTrip(int id, [FromBody] StartTripDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _service.StartTripAsync(userId, id, dto.StartOdometer);

        if (!result)
            return BadRequest("Unable to start trip");

        return Ok();
    }

    [HttpPut("{id}/end")]
    public async Task<IActionResult> EndTrip(int id, [FromBody] EndTripDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _service.EndTripAsync(userId, id, dto.EndOdometer);

        if (!result)
            return BadRequest("Unable to end trip");

        return Ok();
    }

    [HttpGet("vehicle")]
    public async Task<ActionResult<DriverVehicleDto>> GetMyVehicle()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var vehicle = await _service.GetDriverVehicleAsync(userId);
        if (vehicle == null) return NotFound();
        return Ok(vehicle);
    }

    [HttpGet("dashboard")]
    public async Task<ActionResult<DriverDashboardDto>> GetDashboard()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();
        
        var dashboard = await _service.GetDriverDashboardAsync(userId);
        return Ok(dashboard);
    }
}