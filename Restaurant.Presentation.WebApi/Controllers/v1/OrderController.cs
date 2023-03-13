using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Dtos.Order;
using Restaurant.Core.Application.Interfaces.Controllers;
using Restaurant.Core.Domain.Entities;
using Restaurant.Presentation.WebApi.Core;

namespace Restaurant.Presentation.WebApi.Controllers.v1;

[ApiVersion("1.0")]
[Authorize(Roles = "Waiter")]
public class OrderController : GenericController<OrderDto, OrderSaveDto, Order>, IOrderController {
  private readonly IOrderService _orderService;
  public OrderController(IOrderService orderService) : base(orderService) => _orderService = orderService;

  [HttpPost]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async override Task<ActionResult> Create([FromBody] OrderSaveDto dto) {
    if (dto.TableId < 1)
      return BadRequest("The table id is invalid");

    if (dto.Plates == null || dto.Plates.Count == 0)
      return BadRequest("The order must have at least one plate");

    if (dto.StatusId > 2 || dto.StatusId < 1)
      return BadRequest("The order status is invalid");

    return await base.Create(dto);
  }

  [HttpPut("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async override Task<ActionResult<OrderDto>> Update([FromBody] OrderSaveDto dto) {
    if (dto.Plates == null || dto.Plates.Count == 0)
      return BadRequest("The order must have at least one plate");

    dto.Id = int.Parse(RouteData.Values["id"].ToString());

    var order = await _orderService.GetEntity(dto.Id);

    if (order == null)
      return NotFound($"The order with id {RouteData.Values["id"]} was not found");

    order.Plates = dto.Plates;

    return await base.Update(order);
  }
}
