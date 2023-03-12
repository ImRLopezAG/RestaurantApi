using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities;

public class Ingredient : BasicEntity {
  public ICollection<PlateIngredient> PlateIngredients { get; set; } = null!;
}
