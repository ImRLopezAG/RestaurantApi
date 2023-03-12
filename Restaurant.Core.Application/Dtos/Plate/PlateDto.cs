using Restaurant.Core.Application.Dtos.Ingredient;
using Restaurant.Core.Application.Dtos.utils;

namespace Restaurant.Core.Application.Dtos.Plate;

public class PlateDto : BaseDto {
  public double Price { get; set; }
  public int Capacity { get; set; }

  public int CategoryId { get; set; }

  public ICollection<IngredientDto> Ingredients { get; set; } = null!;

  public PlateCategoryDto Category { get; set; } = null!;
}
