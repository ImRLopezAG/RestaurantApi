using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Contracts.Core;
using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Core.Models;

namespace Restaurant.Presentation.WebApi.Core;
[ApiVersion("1.0")]
public class GenericController<TDto, TSaveDto, TEntity> : BaseApiController, IGenericController<TDto, TSaveDto, TEntity> where TDto : class where TSaveDto : Base where TEntity : class {

  private readonly IGenericService<TDto, TSaveDto, TEntity> _service;

  public GenericController(IGenericService<TDto, TSaveDto, TEntity> service) => _service = service;


  [HttpGet]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public virtual async Task<ActionResult<IEnumerable<TDto>>> List([FromQuery] TDto filter) {
    var result = await _service.GetAll().ContinueWith(t => t.Result.Data);
    if (result == null)
      return NotFound();
    return Ok(result);
  }

  [HttpGet("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public virtual async Task<ActionResult<TDto>> GetById(int id) {
    var result = await _service.GetEntity(id);
    return Ok(result);
  }

  [HttpPost]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public virtual async Task<ActionResult<TDto>> Create(TSaveDto dto) {
    try {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);
      var result = await _service.Save(dto);
      return Ok(result);
    } catch (Exception e) {
      return StatusCode(500, e.Message);
    }
  }

  [HttpPut("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public virtual async Task<ActionResult<TDto>> Update(TSaveDto dto) {
    try {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);
      var result = await _service.Edit(dto);
      return Ok(result);
    } catch (Exception e) {
      return StatusCode(500, e.Message);
    }
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public virtual async Task<ActionResult<TDto>> Delete(int id) {
    try {
      var entity = await _service.GetEntity(id);
      if (entity == null)
        return NotFound();
      await _service.Delete(id);
      return Ok(entity);
    } catch (Exception e) {
      return StatusCode(500, e.Message);
    }
  }
}