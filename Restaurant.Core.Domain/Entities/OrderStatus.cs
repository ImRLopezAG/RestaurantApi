using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities;

public class OrderStatus : BasicEntity
{
  public ICollection<Order> Orders { get; set; } = null!;
}
