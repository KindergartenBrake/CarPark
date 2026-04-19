using AutoMapper;
using CarPark.API.DTO;
using CarPark.Domain.Entities;
using CarPark.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripController(
    ITripRepository repository,
    IMapper mapper)
    : BaseController<Trip, TripDto>(repository, mapper)
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripDto>>> GetAll(CancellationToken ct)
    {
        var trips = await repository.GetAll(ct);
        return Ok(_mapper.Map<IEnumerable<TripDto>>(trips));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TripDto>> Get(int id, CancellationToken ct)
    {
        var trip = await repository.Get(id, ct);
        if (trip == null) return NotFound();

        return Ok(_mapper.Map<TripDto>(trip));
    }

    [HttpGet("driver/{driverId}")]
    public async Task<ActionResult<IEnumerable<TripDto>>> GetByDriver(int driverId, CancellationToken ct)
    {
        var trips = await repository.GetTripsByDriver(driverId, ct);
        return Ok(_mapper.Map<IEnumerable<TripDto>>(trips));
    }
}