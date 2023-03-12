using Restaurant.Core.Application.Dtos.utils;

namespace Restaurant.Core.Application.Dtos.Table;

public class TableOrderDto {
  public int Id { get; set; }
  public string Description { get; set; } = null!;
  public int Capacity { get; set; }
  public int StatusId { get; set; }
  public TableStatusDto Status { get; set; } = null!;
}
