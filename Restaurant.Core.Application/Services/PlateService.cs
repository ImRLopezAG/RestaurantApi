using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Dtos.Plate;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Services;

public class PlateService: GenericService<PlateDto, PlateSaveDto, Plate>, IPlateService
{
  private readonly IPlateRepository _plateRepository;
  private readonly IMapper _mapper;

  public PlateService(IPlateRepository plateRepository, IMapper mapper) : base(plateRepository, mapper)
  {
    _plateRepository = plateRepository;
    _mapper = mapper;
  }
}
