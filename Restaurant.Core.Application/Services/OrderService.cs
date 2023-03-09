using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Dtos.Order;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Services;

public class OrderService: GenericService<OrderDto, OrderSaveDto, Order>, IOrderService
{
  private readonly IOrderRepository _orderRepository;
  private readonly IMapper _mapper;

  public OrderService(IOrderRepository orderRepository, IMapper mapper) : base(orderRepository, mapper)
  {
    _orderRepository = orderRepository;
    _mapper = mapper;
  }
}