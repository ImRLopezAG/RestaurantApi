using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Context;
using Restaurant.Infrastructure.Persistence.Core;

namespace Restaurant.Infrastructure.Persistence.Repositories;

public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository {
  private readonly RestaurantContext _context;

  public IngredientRepository(RestaurantContext context) : base(context) => _context = context;

}
