using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Dtos.Ingredient;
using Restaurant.Core.Application.Interfaces.Controllers;
using Restaurant.Core.Domain.Entities;
using Restaurant.Presentation.WebApi.Core;

namespace Restaurant.Presentation.WebApi.Controllers;
public class IngredientController : GenericController<IngredientDto, IngredientSaveDto, Ingredient>, IIngredientController {
  private readonly IIngredientService _ingredientService;

  public IngredientController(IIngredientService ingredientService) : base(ingredientService) {
    _ingredientService = ingredientService;
  }

}
