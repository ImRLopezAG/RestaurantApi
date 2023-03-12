using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Dtos.Order;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Controllers;

public interface IOrderController : IGenericController<OrderDto, OrderSaveDto, Order> {

}
