using AutoMapper;
using CarPark.API.DTO;
using CarPark.Domain.Entities;
using CarPark.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DriverController : ControllerBase
{
    private readonly IDriverService _service;
    private readonly IMapper _mapper;

    public DriverController(IDriverService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DriverDto>>> GetAll(CancellationToken ct)
    {
        var data = await _service.GetAllDrivers(ct);
        return Ok(_mapper.Map<IEnumerable<DriverDto>>(data));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DriverDto>> Get(int id, CancellationToken ct)
    {
        var driver = await _service.GetDriver(id, ct);

        if (driver == null)
            return NotFound();

        return Ok(_mapper.Map<DriverDto>(driver));
    }

    [HttpPost]
    public async Task<IActionResult> Add(DriverDto dto, CancellationToken ct)
    {
        if (dto == null)
            return BadRequest();

        var entity = _mapper.Map<Driver>(dto);

        var result = await _service.CreateDriver(entity, ct);

        return Ok(_mapper.Map<DriverDto>(result));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        try
        {
            await _service.DeleteDriver(id, ct);
            return NoContent();
        }
        catch
        {
            return NotFound();
        }
    }
}