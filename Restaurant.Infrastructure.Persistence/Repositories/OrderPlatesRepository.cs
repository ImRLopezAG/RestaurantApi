using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Context;
using Restaurant.Infrastructure.Persistence.Core;

namespace Restaurant.Infrastructure.Persistence.Repositories;

public class OrderPlatesRepository : GenericRepository<OrderPlate>, IOrderPlateRepository {
  private readonly RestaurantContext _dbContext;
  public OrderPlatesRepository(RestaurantContext dbContext) : base(dbContext) => _dbContext = dbContext;

  public virtual async Task SaveRange(List<OrderPlate> entities) {
    await _dbContext.Set<OrderPlate>().AddRangeAsync(entities);
    await _dbContext.SaveChangesAsync();
  }
  public virtual async Task UpdateRange(List<OrderPlate> entities) {
    foreach (var entity in entities) {
      var entry = await _dbContext.Set<OrderPlate>().FindAsync(entity.Id);
      _dbContext.Entry(entry).CurrentValues.SetValues(entity);
    }
    await _dbContext.SaveChangesAsync();
  }

  public virtual async Task DeleteRange(List<OrderPlate> entities) {
    _dbContext.Set<OrderPlate>().RemoveRange(entities);
    await _dbContext.SaveChangesAsync();
  }

}

