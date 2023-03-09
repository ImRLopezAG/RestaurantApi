using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities;

public class Order : BaseEntity
{
  public double SubTotal { get; set; }
  public int TableId { get; set; }
  public int OrderStatusId { get; set; }

  // Navigation properties
  public Table Table { get; set; } = null!;
  public ICollection<Plate> Plates { get; set; } = null!;
  public OrderStatus OrderStatus { get; set; } = null!;
}

