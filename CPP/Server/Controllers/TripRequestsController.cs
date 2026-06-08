using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CP.Server.DTO;
using CP.Server.Services;
using System.Security.Claims;
using CP.Server.Services.Interfaces;
namespace CP.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TripRequestsController : ControllerBase
{
    private readonly ITripRequestService _service;

    public TripRequestsController(ITripRequestService service)
    {
        _service = service;
    }

    [HttpGet("my")]
    public async Task<ActionResult<List<TripRequestDto>>> GetMyRequests()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var requests = await _service.GetMyRequestsAsync(userId);
        return Ok(requests);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TripRequestDto>> GetById(int id)
    {
        var request = await _service.GetByIdAsync(id);
        if (request == null)
            return NotFound();
        return Ok(request);
    }

    [HttpPost]
    public async Task<ActionResult<TripRequestDto>> Create([FromBody] CreateTripRequestDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        try
        {
            var result = await _service.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = result.RequestId }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Внутренняя ошибка сервера" });
        }
    }

    [HttpGet("dashboard")]
    public async Task<ActionResult<EmployeeDashboardDto>> GetEmployeeDashboard()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();
        
        var dashboard = await _service.GetEmployeeDashboardAsync(userId);
        return Ok(dashboard);
    }
}