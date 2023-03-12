using Restaurant.Core.Application.Core.Models;

namespace Restaurant.Core.Application.Dtos.Order;

public class OrderSaveDto : Base {
  public int TableId { get; set; }
  public int StatusId { get; set; }

  public List<int> Plates { get; set; } = null!;
}
