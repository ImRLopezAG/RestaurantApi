using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Dtos.Plate;
using Restaurant.Core.Application.Interfaces.Controllers;
using Restaurant.Core.Domain.Entities;
using Restaurant.Presentation.WebApi.Core;

namespace Restaurant.Presentation.WebApi.Controllers.v1;

[ApiVersion("1.0")]
[Authorize(Roles = "Admin")]
public class PlateController : GenericController<PlateDto, PlateSaveDto, Plate>, IPlateController {

  private readonly IPlateService _plateService;
  public PlateController(IPlateService plateService) : base(plateService) => _plateService = plateService;

  [HttpPost]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async override Task<ActionResult> Create([FromBody] PlateSaveDto dto) {
    var isNull = dto.Ingredients.Contains(0);
    if (dto.Ingredients == null || dto.Ingredients.Count == 0 || isNull)
      return BadRequest("The plate must have at least one ingredient");

    if (dto.CategoryId > 4 || dto.CategoryId < 1)
      return BadRequest("The plate category is invalid");

    return await base.Create(dto);
  }

  [HttpPut("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async override Task<ActionResult<PlateDto>> Update([FromBody] PlateSaveDto dto) {
    if (dto.Ingredients == null || dto.Ingredients.Count == 0)
      return BadRequest("The plate must have at least one ingredient");

    dto.Id = int.Parse(RouteData.Values["id"].ToString());

    var plate = await _plateService.GetEntity(dto.Id);

    if (plate == null)
      return NotFound($"The plate with id {RouteData.Values["id"]} was not found");

    plate.Ingredients = dto.Ingredients;

    var newPlate = await _plateService.Edit(plate);

    return Ok(newPlate);
  }

}
