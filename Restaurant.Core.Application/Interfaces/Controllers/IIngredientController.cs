using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Dtos.Ingredient;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Controllers;

public interface IIngredientController : IGenericController<IngredientDto, IngredientSaveDto, Ingredient> {

}
