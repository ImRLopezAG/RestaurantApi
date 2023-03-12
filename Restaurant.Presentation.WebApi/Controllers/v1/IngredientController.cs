using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Dtos.Ingredient;
using Restaurant.Core.Application.Interfaces.Controllers;
using Restaurant.Core.Domain.Entities;
using Restaurant.Presentation.WebApi.Core;

namespace Restaurant.Presentation.WebApi.Controllers.v1;

[ApiVersion("1.0")]
public class IngredientController : GenericController<IngredientDto, IngredientSaveDto, Ingredient>, IIngredientController {
  public IngredientController(IIngredientService IngredientService) : base(IngredientService) {
  }

}
