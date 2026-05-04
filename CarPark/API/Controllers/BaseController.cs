using AutoMapper;
using CarPark.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController<TEntity, TDto>(
    IRepository<TEntity> repository,
    IMapper mapper) : ControllerBase where TEntity : class
{
    protected readonly IRepository<TEntity> _repository = repository;
    protected readonly IMapper _mapper = mapper;

    /// <summary>
    /// 1) Проверяет вход
    /// 2) Преобразует DTO в Entity
    /// 3) Сохраняет в БД через репозиторий
    /// 4) Возвращает: HTTP 204 — успешно, но без тела ответа
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost]
    public virtual async Task<IActionResult> Add(TDto dto, CancellationToken ct)
    {
        if (dto == null)
            return BadRequest();

        var entity = _mapper.Map<TEntity>(dto);
        await _repository.Add(entity, ct);

        return NoContent();
    }
    /// <summary>
    /// Получает id из URL
    /// Удаляет запись через репозиторий
    /// Возвращает 204
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public virtual async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var existing = await _repository.Get(id, ct);
        if (existing == null)
            return NotFound();

        await _repository.Delete(id, ct);

        return NoContent();
    }
    /// <summary>
    /// Обновление существующей записи
    /// Проверяется вход, если ничего - 400
    /// Получает объект из бд
    /// Проверяет на существование объект, если нет - ошибка
    /// Обновляет данные mapper, перезаписывает поля в existing(объект)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update(int id, TDto dto, CancellationToken ct)
    {
        if (dto == null)
            return BadRequest();

        var existing = await _repository.Get(id, ct);
        if (existing == null)
            return NotFound();

        _mapper.Map(dto, existing);

        return Ok(_mapper.Map<TDto>(existing));
    }
}