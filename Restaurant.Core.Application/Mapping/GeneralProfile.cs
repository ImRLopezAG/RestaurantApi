using AutoMapper;
using Restaurant.Core.Application.Dtos.Ingredient;
using Restaurant.Core.Application.Dtos.Order;
using Restaurant.Core.Application.Dtos.Plate;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Mapping;

public class GeneralProfile : Profile
{
  public GeneralProfile()
  {
    #region Plate
    CreateMap<Plate, PlateDto>()
      .ForMember(dto => dto.Id, opt => opt.MapFrom(plate => plate.Id))
      .ReverseMap()
      .ForMember(plate => plate.Ingredients, opt => opt.Ignore())
      .ForMember(plate => plate.PlateCategory, opt => opt.Ignore())
      .ForMember(plate => plate.PlateIngredients, opt => opt.Ignore());

    CreateMap<Plate, PlateSaveDto>()
      .ReverseMap()
      .ForMember(plate => plate.Ingredients, opt => opt.Ignore())
      .ForMember(plate => plate.PlateCategory, opt => opt.Ignore())
      .ForMember(plate => plate.PlateIngredients, opt => opt.Ignore());

    CreateMap<PlateSaveDto, PlateDto>();

    #endregion

    #region Ingredient
    CreateMap<Ingredient, IngredientDto>()
      .ForMember(dto => dto.Id, opt => opt.MapFrom(ingredient => ingredient.Id))
      .ReverseMap()
      .ForMember(ingredient => ingredient.PlateIngredients, opt => opt.Ignore());

    CreateMap<Ingredient, IngredientSaveDto>()
      .ReverseMap()
      .ForMember(ingredient => ingredient.PlateIngredients, opt => opt.Ignore());

    CreateMap<IngredientSaveDto, IngredientDto>();

    #endregion

    #region Table
    CreateMap<Table, TableDto>()
      .ForMember(dto => dto.Id, opt => opt.MapFrom(table => table.Id))
      .ReverseMap()
      .ForMember(table => table.TableStatus, opt => opt.Ignore())
      .ForMember(table => table.Orders, opt => opt.Ignore());

    CreateMap<Table, TableSaveDto>()
      .ReverseMap()
      .ForMember(table => table.TableStatus, opt => opt.Ignore())
      .ForMember(table => table.Orders, opt => opt.Ignore());

    CreateMap<TableSaveDto, TableDto>();

    #endregion

    #region Order
    CreateMap<Order, OrderDto>()
      .ForMember(dto => dto.Id, opt => opt.MapFrom(order => order.Id))
      .ReverseMap()
      .ForMember(order => order.OrderStatus, opt => opt.Ignore())
      .ForMember(order => order.Table, opt => opt.Ignore())
      .ForMember(order => order.Plates, opt => opt.Ignore());

    CreateMap<Order, OrderSaveDto>()
      .ReverseMap()
      .ForMember(order => order.OrderStatus, opt => opt.Ignore())
      .ForMember(order => order.Table, opt => opt.Ignore())
      .ForMember(order => order.Plates, opt => opt.Ignore());

    CreateMap<OrderSaveDto, OrderDto>();

    #endregion
      
  }
}
