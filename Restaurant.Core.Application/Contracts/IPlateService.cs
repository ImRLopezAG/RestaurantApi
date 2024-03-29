using Restaurant.Core.Application.Contracts.Core;
using Restaurant.Core.Application.Dtos.Plate;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Contracts;

public interface IPlateService : IGenericService<PlateDto, PlateSaveDto, Plate> {

}
