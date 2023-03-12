using AutoMapper;
using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Dtos.Ingredient;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Services;

public class IngredientService : GenericService<IngredientDto, IngredientSaveDto, Ingredient>, IIngredientService {
  private readonly IIngredientRepository _ingredientRepository;
  private readonly IMapper _mapper;

  public IngredientService(IIngredientRepository ingredientRepository, IMapper mapper) : base(ingredientRepository, mapper) {
    _ingredientRepository = ingredientRepository;
    _mapper = mapper;
  }
}