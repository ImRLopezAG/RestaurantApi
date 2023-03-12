using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Context;
using Restaurant.Infrastructure.Persistence.Core;

namespace Restaurant.Infrastructure.Persistence.Repositories;

public class PlateIngredientRepository : GenericRepository<PlateIngredient>, IPlateIngredientRepository {
  private readonly RestaurantContext _context;
  public PlateIngredientRepository(RestaurantContext dbContext) : base(dbContext) => _context = dbContext;

  public virtual async Task SaveRange(List<PlateIngredient> entities) {
    await _context.Set<PlateIngredient>().AddRangeAsync(entities);
    await _context.SaveChangesAsync();
  }
  public virtual async Task UpdateRange(List<PlateIngredient> entities) {
    foreach (var entity in entities) {
      var entry = await _context.Set<PlateIngredient>().FindAsync(entity.Id);
      _context.Entry(entry).CurrentValues.SetValues(entity);
    }
    await _context.SaveChangesAsync();
  }

  public virtual async Task DeleteRange(List<PlateIngredient> entities) {
    _context.Set<PlateIngredient>().RemoveRange(entities);
    await _context.SaveChangesAsync();
  }
}