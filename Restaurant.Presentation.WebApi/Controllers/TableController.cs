using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Interfaces.Controllers;
using Restaurant.Core.Domain.Entities;
using Restaurant.Presentation.WebApi.Core;

namespace Restaurant.Presentation.WebApi.Controllers;

public class TableController : GenericController<TableDto, TableSaveDto, Table>, ITableController {
  private readonly ITableService _tableService;

  public TableController(ITableService tableService) : base(tableService) {
    _tableService = tableService;
  }

}

