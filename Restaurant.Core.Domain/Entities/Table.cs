using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities;

public class Table: BasicEntity
{
  public int Capacity { get; set; }
  public string Description { get; set; } = null!;
  // Navigation properties
  public int StatusId { get; set; }
  public TableStatus TableStatus { get; set; } = null!;

  public ICollection<Order> Orders { get; set; } = null!;
}
