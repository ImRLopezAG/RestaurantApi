using Restaurant.Core.Application.Contracts.Core;
using Restaurant.Core.Application.Dtos.Order;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Contracts;

public interface IOrderService : IGenericService<OrderDto, OrderSaveDto, Order> {
  Task<IEnumerable<OrderDto>> GetOrdersByTableId(int id);
}
