using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities;

public class PlateIngredient : BaseEntity {
  public int PlateId { get; set; }
  public int IngredientId { get; set; }
  public Plate Plate { get; set; } = null!;
  public Ingredient Ingredient { get; set; } = null!;
}
