using Restaurant.Core.Application.Dtos.Plate;

namespace Restaurant.Core.Application.Dtos.utils;

public class OrderPlateDto : BaseDto {
  public PlateDto Plate { get; set; } = null!;
}
