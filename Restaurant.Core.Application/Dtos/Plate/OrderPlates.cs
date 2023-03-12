using Restaurant.Core.Application.Dtos.utils;

namespace Restaurant.Core.Application.Dtos.Plate;

public class OrderPlates {
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public int CategoryId { get; set; }
  public double Price { get; set; }
  public PlateCategoryDto Category { get; set; } = null!;
}
