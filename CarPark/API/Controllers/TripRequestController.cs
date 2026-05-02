using AutoMapper;
using CarPark.API.DTO;
using CarPark.Domain.Entities;
using CarPark.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripRequestController(
    ITripRequestRepository repository,
    IMapper mapper)
    : BaseController<TripRequest, TripRequestDto>(repository, mapper)
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripRequestDto>>> GetAll(CancellationToken ct)
    {
        var data = await repository.GetAll(ct);
        return Ok(_mapper.Map<IEnumerable<TripRequestDto>>(data));
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<TripRequestDto>>> GetByUser(int userId, CancellationToken ct)
    {
        var data = await repository.GetByUser(userId, ct);
        return Ok(_mapper.Map<IEnumerable<TripRequestDto>>(data));
    }
}