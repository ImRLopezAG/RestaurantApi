using Restaurant.Core.Application.Core;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Repositories;

public interface IPlateIngredientRepository : IGenericRepository<PlateIngredient> {
  Task SaveRange(List<PlateIngredient> entities);
  Task UpdateRange(List<PlateIngredient> entities);
  Task DeleteRange(List<PlateIngredient> entities);
}
