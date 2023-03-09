using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities;

public class TableStatus: BasicEntity
{
  public ICollection<Table> Tables { get; set; } = null!;
}
