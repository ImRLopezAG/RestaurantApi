using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities;

public class PlateCategory: BasicEntity
{
  public ICollection<Plate> Plates { get; set; } = null!;
}
