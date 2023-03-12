using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities;

public class OrderPlate : BaseEntity {
  public int PlateId { get; set; }
  public int OrderId { get; set; }

  public Plate Plate { get; set; } = null!;
  public Order Order { get; set; } = null!;
}
