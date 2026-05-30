using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CP.Server.DTO;
using CP.Server.Services;

namespace CP.Server.Controllers;

[ApiController]
[Route("api/admin/drivers")]
[Authorize]
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
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.DriverId }, result);
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
}