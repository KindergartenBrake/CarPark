using AutoMapper;
using CarPark.API.DTO;
using CarPark.Domain.Entities;
using CarPark.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleController(
    IVehicleRepository repository,
    IMapper mapper)
    : BaseController<Vehicle, VehicleDto>(repository, mapper)
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleDto>>> GetAll(CancellationToken ct)
    {
        var vehicles = await repository.GetAll(ct);
        return Ok(_mapper.Map<IEnumerable<VehicleDto>>(vehicles));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleDto>> Get(int id, CancellationToken ct)
    {
        var vehicle = await repository.Get(id, ct);
        if (vehicle == null) return NotFound();

        return Ok(_mapper.Map<VehicleDto>(vehicle));
    }
}