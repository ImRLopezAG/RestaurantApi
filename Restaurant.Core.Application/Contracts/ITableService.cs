using Restaurant.Core.Application.Contracts.Core;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Contracts;

public interface ITableService : IGenericService<TableDto, TableSaveDto, Table> {

}
