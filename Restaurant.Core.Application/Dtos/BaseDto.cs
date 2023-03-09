using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Core.Application.Core.Models;

namespace Restaurant.Core.Application.Dtos;

public class BaseDto: Base
{
  public string Name { get; set; } = null!;
}
