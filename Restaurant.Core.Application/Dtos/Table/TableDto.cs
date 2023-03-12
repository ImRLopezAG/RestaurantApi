using Restaurant.Core.Application.Core.Models;
using Restaurant.Core.Application.Dtos.Order;
using Restaurant.Core.Application.Dtos.utils;

namespace Restaurant.Core.Application.Dtos.Table;

public class TableDto : Base {
  public int Capacity { get; set; }
  public string Description { get; set; } = null!;
  public int StatusId { get; set; }
  public TableStatusDto Status { get; set; } = null!;
  public ICollection<OrderDto> Orders { get; set; } = null!;
}