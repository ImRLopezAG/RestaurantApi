using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Controllers;

public interface ITableController : IGenericController<TableDto, TableSaveDto, Table> {
  Task<ActionResult<TableDto>> GetTableOrders(int id);
  Task<ActionResult> ChangeStatus(int id, int statusId);
}
