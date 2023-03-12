using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities;

public class Table : BaseEntity {
  public int Capacity { get; set; }
  public string Description { get; set; } = null!;
  public int StatusId { get; set; }
  // Navigation Properties
  public ICollection<Order> Orders { get; set; } = null!;
}
