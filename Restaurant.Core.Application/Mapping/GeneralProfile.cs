using AutoMapper;
using Restaurant.Core.Application.Dtos.Ingredient;
using Restaurant.Core.Application.Dtos.Order;
using Restaurant.Core.Application.Dtos.Plate;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Mapping;

public class GeneralProfile : Profile {
  public GeneralProfile() {
    #region Plate
    CreateMap<Plate, PlateDto>()
      .ForMember(dto => dto.Ingredients, opt => opt.Ignore())
      .ForMember(dto => dto.Category, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(plate => plate.Ingredients, opt => opt.Ignore())
      .ForMember(plate => plate.OrderPlates, opt => opt.Ignore())
      .ForMember(plate => plate.PlateIngredients, opt => opt.Ignore())
      .ForMember(plate => plate.CreatedAt, opt => opt.Ignore())
      .ForMember(plate => plate.LastModifiedAt, opt => opt.Ignore());

    CreateMap<Plate, PlateSaveDto>()
      .ForMember(dto => dto.Ingredients, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(plate => plate.Ingredients, opt => opt.Ignore())
      .ForMember(plate => plate.PlateIngredients, opt => opt.Ignore())
      .ForMember(plate => plate.CreatedAt, opt => opt.Ignore())
      .ForMember(plate => plate.LastModifiedAt, opt => opt.Ignore());

    #endregion

    #region Ingredient
    CreateMap<Ingredient, IngredientDto>()
      .ReverseMap()
      .ForMember(ingredient => ingredient.PlateIngredients, opt => opt.Ignore())
      .ForMember(plate => plate.CreatedAt, opt => opt.Ignore())
      .ForMember(plate => plate.LastModifiedAt, opt => opt.Ignore());

    CreateMap<Ingredient, IngredientSaveDto>()
      .ReverseMap()
      .ForMember(ingredient => ingredient.PlateIngredients, opt => opt.Ignore())
      .ForMember(plate => plate.CreatedAt, opt => opt.Ignore())
      .ForMember(plate => plate.LastModifiedAt, opt => opt.Ignore());

    #endregion

    #region Table
    CreateMap<Table, TableDto>()
      .ForMember(dto => dto.Status, opt => opt.Ignore())
      .ForMember(dto => dto.Orders, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(table => table.Orders, opt => opt.Ignore())
      .ForMember(plate => plate.CreatedAt, opt => opt.Ignore())
      .ForMember(plate => plate.LastModifiedAt, opt => opt.Ignore());



    CreateMap<Table, TableSaveDto>()
      .ReverseMap()
      .ForMember(table => table.Orders, opt => opt.Ignore())
      .ForMember(plate => plate.CreatedAt, opt => opt.Ignore())
      .ForMember(plate => plate.LastModifiedAt, opt => opt.Ignore());



    #endregion

    #region Order
    CreateMap<Order, OrderDto>()
      .ForMember(dto => dto.Status, opt => opt.Ignore())
      .ForMember(dto => dto.Table, opt => opt.Ignore())
      .ForMember(dto => dto.Plates, opt => opt.Ignore())
      .ForMember(dto => dto.SubTotal, opt => opt.Ignore())
      .ReverseMap()
      .ForMember(order => order.Table, opt => opt.Ignore())
      .ForMember(order => order.Plates, opt => opt.Ignore())
      .ForMember(plate => plate.CreatedAt, opt => opt.Ignore())
      .ForMember(plate => plate.LastModifiedAt, opt => opt.Ignore());

    CreateMap<Order, OrderSaveDto>()
      .ReverseMap()
      .ForMember(order => order.Table, opt => opt.Ignore())
      .ForMember(order => order.Plates, opt => opt.Ignore())
      .ForMember(order => order.OrderPlates, opt => opt.Ignore())
      .ForMember(order => order.OrderPlates, opt => opt.Ignore())
      .ForMember(plate => plate.CreatedAt, opt => opt.Ignore())
      .ForMember(plate => plate.LastModifiedAt, opt => opt.Ignore());


    #endregion

  }
}
