using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CP.Server.DTO;
using CP.Server.Services;
using CP.Server.Services.Interfaces;
namespace CP.Server.Controllers;

[ApiController]
[Route("api/admin/trips")]
[Authorize(Roles = "Admin")]
public class AdminTripsController : ControllerBase
{
    private readonly IAdminTripService _service;

    public AdminTripsController(IAdminTripService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<TripDto>>> GetAllTrips()
    {
        var trips = await _service.GetAllTripsAsync();
        return Ok(trips);
    }

    [HttpGet("drivers")]
    public async Task<ActionResult<List<string>>> GetDrivers()
    {
        var drivers = await _service.GetAvailableDriversAsync();
        return Ok(drivers);
    }

    [HttpGet("vehicles")]
    public async Task<ActionResult<List<string>>> GetVehicles()
    {
        var vehicles = await _service.GetAvailableVehiclesAsync();
        return Ok(vehicles);
    }

    [HttpPut("{id}/force-complete")]
    public async Task<IActionResult> ForceCompleteTrip(int id, [FromBody] ForceCompleteDto dto)
    {
        await _service.ForceCompleteTripAsync(id, dto.Comment);
        return Ok();
    }

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> CancelTrip(int id)
    {
        await _service.CancelTripAsync(id);
        return Ok();
    }
    [HttpPut("{id}/restore")]
    public async Task<IActionResult> RestoreTrip(int id)
    {
        await _service.RestoreTripAsync(id);
        return Ok();
    }
    
}