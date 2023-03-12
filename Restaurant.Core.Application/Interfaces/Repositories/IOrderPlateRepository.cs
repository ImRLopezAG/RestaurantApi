using Restaurant.Core.Application.Core;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Repositories;

public interface IOrderPlateRepository : IGenericRepository<OrderPlate> {
  Task SaveRange(List<OrderPlate> entities);
  Task UpdateRange(List<OrderPlate> entities);
  Task DeleteRange(List<OrderPlate> entities);
}
