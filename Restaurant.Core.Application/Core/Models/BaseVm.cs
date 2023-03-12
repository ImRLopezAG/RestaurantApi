namespace Restaurant.Core.Application.Core.Models;

public class BaseVm : Base {
  public DateTime CreatedAt { get; set; }
  public DateTime LastModifiedAt { get; set; }
}
