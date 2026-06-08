using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CP.Server.DTO;
using CP.Server.Services;
using CP.Server.Services.Interfaces;
namespace CP.Server.Controllers;

[ApiController]
[Route("api/admin/vehicles")]
[Authorize(Roles = "Admin")]
public class AdminVehiclesController : ControllerBase
{
    private readonly IAdminVehicleService _service;

    public AdminVehiclesController(IAdminVehicleService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<AdminVehicleDto>>> GetAll([FromQuery] string? search = null)
    {
        return Ok(await _service.GetAllVehiclesAsync(search));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AdminVehicleDto>> GetById(int id)
    {
        var vehicle = await _service.GetByIdAsync(id);
        if (vehicle == null) return NotFound();
        return Ok(vehicle);
    }

   [HttpPost]
    public async Task<ActionResult<AdminVehicleDto>> Create([FromBody] CreateVehicleDto dto)
    {
        try
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.VehicleId }, result);
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
    public async Task<IActionResult> Update(int id, [FromBody] CreateVehicleDto dto)
    {
        try
        {
            await _service.UpdateAsync(id, dto);
            return Ok();
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
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("{id}/deactivate")]
    public async Task<IActionResult> Deactivate(int id)
    {
        await _service.DeactivateAsync(id);
        return Ok();
    }
}