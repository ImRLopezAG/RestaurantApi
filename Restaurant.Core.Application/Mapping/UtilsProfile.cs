using AutoMapper;
using Restaurant.Core.Application.Dtos.Plate;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Dtos.utils;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Mapping;

public class UtilsProfile : Profile {
  public UtilsProfile() {
    #region Utils
    CreateMap<PlateIngredient, PlateIngredientDto>()
      .ReverseMap()
      .ForMember(plateIngredient => plateIngredient.Plate, opt => opt.Ignore())
      .ForMember(plateIngredient => plateIngredient.Ingredient, opt => opt.Ignore())
      .ForMember(plate => plate.CreatedAt, opt => opt.Ignore())
      .ForMember(plate => plate.LastModifiedAt, opt => opt.Ignore());

    CreateMap<OrderPlate, OrderPlateDto>()
      .ForMember(dto => dto.Plate, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(orderPlate => orderPlate.Order, opt => opt.Ignore())
      .ForMember(orderPlate => orderPlate.Plate, opt => opt.Ignore())
      .ForMember(plate => plate.CreatedAt, opt => opt.Ignore())
      .ForMember(plate => plate.LastModifiedAt, opt => opt.Ignore());

    CreateMap<Table, TableOrderDto>()
      .ForMember(dto => dto.Status, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(table => table.Orders, opt => opt.Ignore())
      .ForMember(plate => plate.CreatedAt, opt => opt.Ignore())
      .ForMember(plate => plate.LastModifiedAt, opt => opt.Ignore());

    CreateMap<Plate, OrderPlates>()
      .ForMember(dto => dto.Category, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(plate => plate.Ingredients, opt => opt.Ignore())
      .ForMember(plate => plate.OrderPlates, opt => opt.Ignore())
      .ForMember(plate => plate.PlateIngredients, opt => opt.Ignore())
      .ForMember(plate => plate.CreatedAt, opt => opt.Ignore())
      .ForMember(plate => plate.LastModifiedAt, opt => opt.Ignore());

    #endregion
  }
}
