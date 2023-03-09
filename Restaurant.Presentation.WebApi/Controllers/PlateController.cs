using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Dtos.Plate;
using Restaurant.Core.Application.Interfaces.Controllers;
using Restaurant.Core.Domain.Entities;
using Restaurant.Presentation.WebApi.Core;


namespace Restaurant.Presentation.WebApi.Controllers;
[ApiVersion("1.0")]

public class PlateController : GenericController<PlateDto, PlateSaveDto, Plate>, IPlateController {
  private readonly IPlateService _plateService;

  public PlateController(IPlateService plateService) : base(plateService) {
    _plateService = plateService;
  }

}
