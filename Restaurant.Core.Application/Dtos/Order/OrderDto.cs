using Restaurant.Core.Application.Core.Models;
using Restaurant.Core.Application.Dtos.Plate;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Dtos.utils;

namespace Restaurant.Core.Application.Dtos.Order;

public class OrderDto : Base {
  public double SubTotal { get; set; }
  public int TableId { get; set; }
  public int StatusId { get; set; }

  public OrderStatusDto Status { get; set; } = null!;
  public TableOrderDto Table { get; set; } = null!;
  public List<OrderPlates> Plates { get; set; } = null!;
}
