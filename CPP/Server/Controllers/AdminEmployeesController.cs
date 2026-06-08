using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CP.Server.DTO;
using CP.Server.Services;

namespace CP.Server.Controllers;

[ApiController]
[Route("api/admin/employees")]
[Authorize(Roles = "Admin")]
public class AdminEmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public AdminEmployeesController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<EmployeeDto>>> GetAll([FromQuery] string? search = null)
    {
        var employees = await _service.GetAllEmployeesAsync(search);
        return Ok(employees);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<EmployeeDto>> GetById(string userId)
    {
        var employee = await _service.GetEmployeeByIdAsync(userId);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    [HttpPut("{userId}/deactivate")]
    public async Task<IActionResult> Deactivate(string userId)
    {
        await _service.DeactivateEmployeeAsync(userId);
        return Ok();
    }

    [HttpPut("{userId}/activate")]
    public async Task<IActionResult> Activate(string userId)
    {
        await _service.ActivateAsync(userId);
        return Ok();
    }
}