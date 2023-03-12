using Restaurant.Core.Application.Dtos.utils;
using Restaurant.Core.Application.Enums;

namespace Restaurant.Core.Application.Extensions;

public static class GetEnums {
  public static TableStatusDto GetTableStatus(int id) {
    var status = ( TableStatus )id;
    return new TableStatusDto {
      Id = id,
      Status = status.ToString()
    };
  }

  public static OrderStatusDto GetOrderStatus(int id) {
    var status = ( OrderStatus )id;
    return new OrderStatusDto {
      Id = id,
      Status = status.ToString()
    };
  }

  public static PlateCategoryDto GetPlateCategory(int id) {
    var status = ( PlateCategory )id;
    return new PlateCategoryDto {
      Id = id,
      Name = status.ToString()
    };
  }
}
