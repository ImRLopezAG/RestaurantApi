using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Interfaces.Controllers;
using Restaurant.Core.Domain.Entities;
using Restaurant.Presentation.WebApi.Core;

namespace Restaurant.Presentation.WebApi.Controllers.v1;

[ApiVersion("1.0")]
public class TableController : GenericController<TableDto, TableSaveDto, Table>, ITableController {
  private readonly ITableService _tableService;
  private readonly IOrderService _orderService;
  public TableController(ITableService tableService, IOrderService orderService) : base(tableService) {
    _tableService = tableService;
    _orderService = orderService;
  }

  [Authorize]
  [HttpGet]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async override Task<ActionResult<IEnumerable<TableDto>>> List() => await base.List();

  [Authorize]
  [HttpGet("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async override Task<ActionResult<TableDto>> GetById(int id) => await base.GetById(id);



  [Authorize(Roles = "Admin")]
  [HttpPost]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async override Task<ActionResult> Create(TableSaveDto dto) {
    if (dto.StatusId > 3 || dto.StatusId < 1)
      return BadRequest("The table status is invalid");

    return await base.Create(dto);
  }

  [Authorize(Roles = "Admin")]
  [HttpPut("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async override Task<ActionResult<TableDto>> Update(TableSaveDto dto) {


    dto.Id = int.Parse(RouteData.Values["id"].ToString());
    var entity = await _tableService.GetEntity(dto.Id);
    if (entity == null)
      return NotFound();

    entity.Description = dto.Description;
    entity.Capacity = dto.Capacity;

    return await base.Update(entity);
  }

  [Authorize(Roles = "Waiter")]
  [HttpGet("{id}/orders")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<TableDto>> GetTableOrders(int id) {
    TableDto table = await _tableService.GetById(id);
    if (table == null)
      return NotFound();

    var orders = await _orderService.GetOrdersByTableId(id);
    table.Orders = orders.ToList();

    return Ok(table);
  }

  [Authorize(Roles = "Waiter")]
  [HttpPut("{id}/status/{status}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult> ChangeStatus(int id, int status) {
    if (status > 3 || status < 1)
      return BadRequest("The table status is invalid");

    TableSaveDto table = await _tableService.GetEntity(id);
    table.StatusId = status;

    try {
      await _tableService.Edit(table);
    } catch (Exception ex) {
      return StatusCode(500, $"Error while updating table status: {ex.Message}");
    }

    return NoContent();
  }
}

