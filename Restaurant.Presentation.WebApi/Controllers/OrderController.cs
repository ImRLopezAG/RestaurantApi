using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Dtos.Order;
using Restaurant.Core.Application.Interfaces.Controllers;
using Restaurant.Core.Domain.Entities;
using Restaurant.Presentation.WebApi.Core;


namespace Restaurant.Presentation.WebApi.Controllers;

public class OrderController : GenericController<OrderDto, OrderSaveDto, Order>, IOrderController {
  private readonly IOrderService _orderService;

  public OrderController(IOrderService orderService) : base(orderService) {
    _orderService = orderService;
  }

}