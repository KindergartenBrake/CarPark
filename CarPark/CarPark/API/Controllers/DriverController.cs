using AutoMapper;
using CarPark.API.DTO;
using CarPark.Domain.Entities;
using CarPark.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DriverController(
    IDriverRepository repository,
    IMapper mapper)
    : BaseController<Driver, DriverDto>(repository, mapper)
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DriverDto>>> GetAll(CancellationToken ct)
    {
        var data = await repository.GetAll(ct);
        return Ok(_mapper.Map<IEnumerable<DriverDto>>(data));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DriverDto>> Get(int id, CancellationToken ct)
    {
        var driver = await repository.Get(id, ct);
        if (driver == null) return NotFound();

        return Ok(_mapper.Map<DriverDto>(driver));
    }
}