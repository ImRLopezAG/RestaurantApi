using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Context;
using Restaurant.Infrastructure.Persistence.Core;

namespace Restaurant.Infrastructure.Persistence.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository {
  private readonly RestaurantContext _context;
  public OrderRepository(RestaurantContext dbContext) : base(dbContext) => _context = dbContext;
}