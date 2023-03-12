using Restaurant.Core.Application.Core.Models;

namespace Restaurant.Core.Application.Dtos.Table;

public class TableSaveDto : Base {
  public int Capacity { get; set; }
  public string Description { get; set; } = null!;
  public int StatusId { get; set; }
}