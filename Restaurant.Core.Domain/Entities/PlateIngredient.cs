using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities;

public class PlateIngredient: BasicEntity
{
  public int PlateId { get; set; }
  public int IngredientId { get; set; }
  public Plate Plate { get; set; } = null!;
  public Ingredient Ingredient { get; set; } = null!;
}
