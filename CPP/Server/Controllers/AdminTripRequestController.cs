using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CP.Server.DTO;
using CP.Server.Services;

namespace CP.Server.Controllers;

[ApiController]
[Route("api/admin/triprequests")]  
[Authorize(Roles = "Admin")] 
public class AdminTripRequestsController : ControllerBase
{
    private readonly ITripRequestService _service;

    public AdminTripRequestsController(ITripRequestService service)
    {
        _service = service;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<TripRequestForAdminDto>>> GetAllRequests()
    {
        var requests = await _service.GetAllRequestsAsync();
        return Ok(requests);
    }

    [HttpPut("{id}/assign")]
    public async Task<ActionResult> AssignTrip(int id, [FromBody] AssignTripDto dto)
    {
        var result = await _service.AssignTripAsync(id, dto.VehicleId, dto.DriverId);
        if (!result)
            return NotFound();
        return Ok();
    }

    [HttpPut("{id}/reject")]
    public async Task<ActionResult> RejectTrip(int id, [FromBody] RejectTripRequestDto dto)
    {
        var result = await _service.RejectTripAsync(id, dto.Reason);
        if (!result)
            return NotFound();
        return Ok();
    }

    [HttpGet("available-vehicles")]
    public async Task<ActionResult<List<AvailableVehicleDto>>> GetAvailableVehicles()
    {
        var vehicles = await _service.GetAvailableVehiclesAsync();
        return Ok(vehicles);
    }

    [HttpGet("available-drivers")]
    public async Task<ActionResult<List<AvailableDriverDto>>> GetAvailableDrivers()
    {
        var drivers = await _service.GetAvailableDriversAsync();
        return Ok(drivers);
    }
}