using Restaurant.Core.Application.Contracts.Core;
using Restaurant.Core.Application.Dtos.Ingredient;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Contracts;

public interface IIngredientService : IGenericService<IngredientDto, IngredientSaveDto, Ingredient> {

}
