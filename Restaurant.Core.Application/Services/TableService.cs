using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Services;

public class TableService: GenericService<TableDto, TableSaveDto, Table>, ITableService
{
  private readonly ITableRepository _tableRepository;
  private readonly IMapper _mapper;

  public TableService(ITableRepository tableRepository, IMapper mapper) : base(tableRepository, mapper)
  {
    _tableRepository = tableRepository;
    _mapper = mapper;
  }
}
