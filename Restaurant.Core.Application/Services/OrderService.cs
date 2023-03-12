using AutoMapper;
using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Dtos.Order;
using Restaurant.Core.Application.Dtos.Plate;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Extensions;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Services;

public class OrderService : GenericService<OrderDto, OrderSaveDto, Order>, IOrderService {
  private readonly IOrderRepository _orderRepository;
  private readonly IOrderPlateRepository _orderPlateRepository;
  private readonly ITableRepository _tableRepository;
  private readonly IPlateRepository _plateRepository;
  private readonly IIngredientRepository _ingredientRepository;
  private readonly IPlateIngredientRepository _pIngredientRepository;

  private readonly IMapper _mapper;

  public OrderService(IOrderRepository orderRepository, IOrderPlateRepository orderPlateRepository, ITableRepository tableRepository, IPlateRepository plateRepository, IIngredientRepository ingredientRepository, IPlateIngredientRepository pIngredientRepository, IMapper mapper) : base(orderRepository, mapper) {
    _orderRepository = orderRepository;
    _orderPlateRepository = orderPlateRepository;
    _tableRepository = tableRepository;
    _plateRepository = plateRepository;
    _ingredientRepository = ingredientRepository;
    _pIngredientRepository = pIngredientRepository;
    _mapper = mapper;
  }

  public async override Task<OrderSaveDto> Save(OrderSaveDto dto) {
    var entity = await base.Save(dto);

    var order = _mapper.Map<Order>(entity);
    var plates = await _plateRepository.GetAll();

    var validPlates = plates.Where(i => dto.Plates.Contains(i.Id)).ToList();

    List<OrderPlate> orderPlates = new();

    foreach (var plate in validPlates) {
      var orderPlate = new OrderPlate {
        OrderId = order.Id,
        PlateId = plate.Id,
      };
      orderPlates.Add(orderPlate);
    }
    await _orderPlateRepository.SaveRange(_mapper.Map<List<OrderPlate>>(orderPlates));


    entity.Plates = validPlates.Select(p => _mapper.Map<PlateDto>(p).Id).ToList().OrderBy(e => e).ToList();

    return entity;
  }

  public async override Task<OrderSaveDto> Edit(OrderSaveDto dto) {
    var entity = await base.Edit(dto);
    var order = _mapper.Map<Order>(entity);
    var plates = await _plateRepository.GetAll();

    var validPlates = plates.Where(i => dto.Plates.Contains(i.Id)).ToList();

    List<OrderPlate> orderPlates = new();

    foreach (var plate in validPlates) {
      var orderPlate = new OrderPlate {
        OrderId = order.Id,
        PlateId = plate.Id,
      };
      orderPlates.Add(orderPlate);
    }

    await _orderPlateRepository.SaveRange(_mapper.Map<List<OrderPlate>>(orderPlates));

    var ordPlates = await _orderPlateRepository.GetAll().ContinueWith(t => t.Result.Where(i => i.OrderId == order.Id).ToList());

    await _orderPlateRepository.DeleteRange(ordPlates.Where(i => !orderPlates.Any(op => op.PlateId == i.PlateId)).ToList());

    entity.Plates = validPlates.Select(p => _mapper.Map<PlateDto>(p).Id).ToList().OrderBy(e => e).ToList();
    return entity;
  }

  public async override Task<ServiceResult> GetAll() {
    ServiceResult result = new();
    try {
      var orderPlates = await _orderPlateRepository.GetAll();
      var tables = await _tableRepository.GetAll();
      var plates = await _plateRepository.GetAll();
      var ingredients = await _ingredientRepository.GetAll();

      var pIngredients = await _pIngredientRepository.GetAll();

      var query = from order in await _orderRepository.GetAll()
                  select _mapper.Map<OrderDto>(order, opt => opt.AfterMap((src, ord) => {
                    ord.Status = GetEnums.GetOrderStatus(ord.StatusId);
                    ord.Table = _mapper.Map<TableOrderDto>(tables.FirstOrDefault(t => t.Id == ord.TableId), opt => opt.AfterMap((src, table) => {
                      table.Status = GetEnums.GetTableStatus(table.StatusId);
                    }));
                    ord.Plates = plates.Where(p => orderPlates.Any(op => op.OrderId == ord.Id && op.PlateId == p.Id)).Select(p => _mapper.Map<OrderPlates>(p, opt => opt.AfterMap((src, plate) => {
                      plate.Category = GetEnums.GetPlateCategory(plate.CategoryId);
                    }))).ToList();
                    ord.SubTotal = plates.Where(p => orderPlates.Any(op => op.OrderId == ord.Id && op.PlateId == p.Id)).Sum(p => p.Price);
                  }));


      result.Data = query.Count() > 0 ? query.ToList().OrderBy(e => e.Id) : null;
    } catch (Exception e) {
      result.Success = false;
      result.Message = e.Message;
    }
    return result;
  }

  public async override Task<OrderDto> GetById(int id) {

    var orderPlates = await _orderPlateRepository.GetAll();
    var tables = await _tableRepository.GetAll();
    var plates = await _plateRepository.GetAll();
    var ingredients = await _ingredientRepository.GetAll();

    var pIngredients = await _pIngredientRepository.GetAll();

    var query = await _orderRepository.GetEntity(id).ContinueWith(t => _mapper.Map<OrderDto>(t.Result, opt => opt.AfterMap((src, ord) => {
      ord.Status = GetEnums.GetOrderStatus(ord.StatusId);
      ord.Table = _mapper.Map<TableOrderDto>(tables.FirstOrDefault(t => t.Id == ord.TableId), opt => opt.AfterMap((src, table) => {
        table.Status = GetEnums.GetTableStatus(table.StatusId);
      }));
      ord.Plates = plates.Where(p => orderPlates.Any(op => op.OrderId == ord.Id && op.PlateId == p.Id)).Select(p => _mapper.Map<OrderPlates>(p, opt => opt.AfterMap((src, plate) => {
        plate.Category = GetEnums.GetPlateCategory(plate.CategoryId);
      }))).ToList();
      ord.SubTotal = plates.Where(p => orderPlates.Any(op => op.OrderId == ord.Id && op.PlateId == p.Id)).Sum(p => p.Price);
    })));

    return query;
  }

  public async Task<IEnumerable<OrderDto>> GetOrdersByTableId(int id) {
    var orderPlates = await _orderPlateRepository.GetAll();
    var tables = await _tableRepository.GetAll();
    var plates = await _plateRepository.GetAll();
    var ingredients = await _ingredientRepository.GetAll();

    var pIngredients = await _pIngredientRepository.GetAll();

    var query = from order in await _orderRepository.GetAll()
                where order.TableId == id
                select _mapper.Map<OrderDto>(order, opt => opt.AfterMap((src, ord) => {
                  ord.Status = GetEnums.GetOrderStatus(ord.StatusId);
                  ord.Table = _mapper.Map<TableOrderDto>(tables.FirstOrDefault(t => t.Id == ord.TableId), opt => opt.AfterMap((src, table) => {
                    table.Status = GetEnums.GetTableStatus(table.StatusId);
                  }));
                  ord.Plates = plates.Where(p => orderPlates.Any(op => op.OrderId == ord.Id && op.PlateId == p.Id)).Select(p => _mapper.Map<OrderPlates>(p, opt => opt.AfterMap((src, plate) => {
                    plate.Category = GetEnums.GetPlateCategory(plate.CategoryId);
                  }))).ToList();
                  ord.SubTotal = plates.Where(p => orderPlates.Any(op => op.OrderId == ord.Id && op.PlateId == p.Id)).Sum(p => p.Price);
                }));

    return query;
  }
}