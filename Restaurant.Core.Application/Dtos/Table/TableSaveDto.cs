using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Core.Application.Core.Models;

namespace Restaurant.Core.Application.Dtos.Table;

public class TableSaveDto: Base
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; }
}