using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Context;
using Restaurant.Infrastructure.Persistence.Core;

namespace Restaurant.Infrastructure.Persistence.Repositories;

public class PlateRepository : GenericRepository<Plate>, IPlateRepository {
  private readonly RestaurantContext _context;
  public PlateRepository(RestaurantContext dbContext) : base(dbContext) => _context = dbContext;

}