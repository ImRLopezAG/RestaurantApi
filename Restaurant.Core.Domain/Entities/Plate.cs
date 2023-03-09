using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities;

public class Plate: BasicEntity
{
  public double Price { get; set; }
  public int Capacity { get; set; }
  
  public int PlateCategoryId { get; set; }

  // Navigation properties
  public ICollection<Ingredient> Ingredients { get; set; } = null!;
  public PlateCategory PlateCategory { get; set; } = null!;

  public ICollection<PlateIngredient> PlateIngredients { get; set; } = null!;
}
