using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CP.Server.DTO;
using CP.Server.Services;

namespace CP.Server.Controllers;

[ApiController]
[Route("api/admin/drivers")]
[Authorize(Roles = "Admin")]
public class AdminDriversController : ControllerBase
{
    private readonly IAdminDriverService _service;

    public AdminDriversController(IAdminDriverService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<List<AdminDriverDto>>> GetAll([FromQuery] string? search = null)
        => Ok(await _service.GetAllAsync(search));

    [HttpPost]
    public async Task<ActionResult<AdminDriverDto>> Create([FromBody] CreateDriverDto dto)
    {
        try
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.DriverId }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Внутренняя ошибка сервера" });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateDriverDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpPut("{id}/deactivate")]
    public async Task<IActionResult> Deactivate(int id)
    {
        await _service.DeactivateAsync(id);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AdminDriverDto>> GetById(int id)
    {
        var driver = await _service.GetByIdAsync(id);
        if (driver == null) return NotFound();
        return Ok(driver);
    }

    [HttpGet("users")]
    public async Task<ActionResult<List<UserLookupDto>>> GetAvailableUsers()
    {
        var users = await _service.GetAvailableUsersAsync();
        return Ok(users);
    }

    [HttpGet("all-users")]
    public async Task<ActionResult<List<UserLookupDto>>> GetAllUsers()
    {
        var users = await _service.GetAllUsersAsync();
        return Ok(users);
    }
}