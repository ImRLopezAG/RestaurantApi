using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities;

public class Order : BaseEntity {
  public int TableId { get; set; }
  public int StatusId { get; set; }

  // Navigation properties
  public Table Table { get; set; } = null!;
  public ICollection<Plate> Plates { get; set; } = null!;

  public ICollection<OrderPlate> OrderPlates { get; set; } = null!;
}

