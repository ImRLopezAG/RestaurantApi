using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Restaurant.Core.Application.Contracts.Core;
using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Core.Models;

namespace Restaurant.Presentation.WebApi.Core;
[Authorize(Roles = "Admin, Waiter")]

public class GenericController<TDto, TSaveDto, TEntity> : BaseApiController, IGenericController<TDto, TSaveDto, TEntity> where TDto : class where TSaveDto : Base where TEntity : class {

  private readonly IGenericService<TDto, TSaveDto, TEntity> _service;
  public GenericController(IGenericService<TDto, TSaveDto, TEntity> service) => _service = service;


  [HttpGet]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public virtual async Task<ActionResult<IEnumerable<TDto>>> List() {
    try {
      var result = await _service.GetAll().ContinueWith(x => x.Result.Data);
      if (result == null)
        return NotFound("There are no entities");

      return Ok(result.ToJson());
    } catch (Exception e) {
      return StatusCode(500, $"Error while getting entities : {e.Message}");
    }
  }

  [HttpGet("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public virtual async Task<ActionResult<TDto>> GetById(int id) {
    try {
      var result = await _service.GetById(id);
      if (result == null)
        return NotFound($"The entity with id {id} does not exist");
      return Ok(result.ToJson());
    } catch (Exception e) {
      return StatusCode(500, $"Error while getting entity : {e.Message}");
    }
  }

  [HttpPost]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public virtual async Task<ActionResult> Create([FromQuery] TSaveDto dto) {
    try {
      if (!ModelState.IsValid)
        return BadRequest("Invalid data");

      var result = await _service.Save(dto);

      if (result == null)
        return BadRequest("Invalid data");

      return StatusCode(201, $"The entity with id {result.Id} was created");
    } catch (Exception e) {
      return StatusCode(500, $"Error while creating entity : {e.Message}");
    }
  }

  [HttpPut("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public virtual async Task<ActionResult<TDto>> Update([FromQuery] TSaveDto dto) {
    try {
      if (!ModelState.IsValid)
        return BadRequest("Invalid data");

      dto.Id = int.Parse(RouteData.Values["id"].ToString());

      var result = await _service.Edit(dto);

      if (result == null)
        return BadRequest("Invalid data");

      return Ok(result);
    } catch (Exception e) {
      return StatusCode(500, $"Error while updating entity : {e.Message}");
    }
  }
}