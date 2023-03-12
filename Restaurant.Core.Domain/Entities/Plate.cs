using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities;

public class Plate : BasicEntity {
  public double Price { get; set; }
  public int Capacity { get; set; }

  public int CategoryId { get; set; }

  // Navigation properties
  public ICollection<Ingredient> Ingredients { get; set; } = null!;

  public ICollection<PlateIngredient> PlateIngredients { get; set; } = null!;

  public ICollection<OrderPlate> OrderPlates { get; set; } = null!;
}
