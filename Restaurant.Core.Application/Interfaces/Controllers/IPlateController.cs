using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Dtos.Plate;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Controllers;

public interface IPlateController : IGenericController<PlateDto, PlateSaveDto, Plate> {

}
