using Restaurant.Core.Application.Core.Models;

namespace Restaurant.Core.Application.Dtos.utils;

public class PlateIngredientDto : Base {
  public int IngredientId { get; set; }
  public int PlateId { get; set; }
}
