using AutoMapper;
using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Extensions;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Services;

public class TableService : GenericService<TableDto, TableSaveDto, Table>, ITableService {
  private readonly ITableRepository _tableRepository;
  private readonly IMapper _mapper;

  public TableService(ITableRepository tableRepository, IMapper mapper) : base(tableRepository, mapper) {
    _tableRepository = tableRepository;
    _mapper = mapper;
  }

  public async override Task<ServiceResult> GetAll() {
    ServiceResult result = new();
    try {
      var query = from table in await _tableRepository.GetAll()
                  select _mapper.Map<TableDto>(table, opt => opt.AfterMap((src, tbl) => {
                    tbl.Status = GetEnums.GetTableStatus(tbl.StatusId);
                  }));
      result.Data = query.Count() > 0 ? query.ToList().OrderBy(e => e.Id) : null;
    } catch (Exception e) {
      result.Success = false;
      result.Message = e.Message;
    }
    return result;
  }

  public async override Task<TableDto> GetById(int id) {

    var query = await _tableRepository.GetEntity(id).ContinueWith(table =>
    _mapper.Map<TableDto>(table.Result, opt => opt.AfterMap((src, tbl) => {
      tbl.Status = GetEnums.GetTableStatus(tbl.StatusId);
    })));

    return query;
  }

}
