using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Controllers;

public interface ITableController: IGenericController<TableDto, TableSaveDto, Table>
{
    
}
