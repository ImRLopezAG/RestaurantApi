using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Dtos.Ingredient;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Controllers;

public interface IIngredientController: IGenericController<IngredientDto, IngredientSaveDto, Ingredient>
{
    
}
