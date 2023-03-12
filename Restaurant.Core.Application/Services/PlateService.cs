using AutoMapper;
using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Dtos.Ingredient;
using Restaurant.Core.Application.Dtos.Plate;
using Restaurant.Core.Application.Extensions;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Services;

public class PlateService : GenericService<PlateDto, PlateSaveDto, Plate>, IPlateService {
  private readonly IPlateRepository _plateRepository;
  private readonly IPlateIngredientRepository _pIngredientRepository;
  private readonly IIngredientRepository _ingredientRepository;
  private readonly IMapper _mapper;

  public PlateService(IPlateRepository plateRepository, IPlateIngredientRepository pIngredientRepository, IIngredientRepository ingredientRepository, IMapper mapper) : base(plateRepository, mapper) {
    _plateRepository = plateRepository;
    _pIngredientRepository = pIngredientRepository;
    _ingredientRepository = ingredientRepository;
    _mapper = mapper;
  }
  public async override Task<PlateSaveDto> Save(PlateSaveDto dto) {
    var entity = await base.Save(dto);

    var plate = _mapper.Map<Plate>(entity);
    var ingredients = await _ingredientRepository.GetAll();

    var validIngredients = ingredients.Where(i => dto.Ingredients.Contains(i.Id)).ToList();

    List<PlateIngredient> plateIngredients = new();

    foreach (var ingredient in validIngredients) {
      var plateIngredient = new PlateIngredient {
        PlateId = plate.Id,
        IngredientId = ingredient.Id,
      };
      plateIngredients.Add(plateIngredient);
    }

    await _pIngredientRepository.SaveRange(_mapper.Map<List<PlateIngredient>>(plateIngredients));

    entity.Ingredients = validIngredients.Select(i => _mapper.Map<IngredientDto>(i).Id).ToList().OrderBy(i => i).ToList();

    return entity;
  }

  public async override Task<PlateSaveDto> Edit(PlateSaveDto dto) {

    var entity = await base.Edit(dto);

    var plate = _mapper.Map<Plate>(entity);
    var ingredients = await _ingredientRepository.GetAll();

    var validIngredients = ingredients.Where(i => dto.Ingredients.Contains(i.Id)).ToList();

    List<PlateIngredient> plateIngredients = new();

    foreach (var ingredient in validIngredients) {
      var plateIngredient = new PlateIngredient {
        PlateId = plate.Id,
        IngredientId = ingredient.Id,
      };
      plateIngredients.Add(plateIngredient);
    }

    await _pIngredientRepository.SaveRange(_mapper.Map<List<PlateIngredient>>(plateIngredients));

    var pIngredients = await _pIngredientRepository.GetAll().ContinueWith(pi => pi.Result.Where(p => p.PlateId == plate.Id).ToList());

    await _pIngredientRepository.DeleteRange(pIngredients.Where(pi => !plateIngredients.Any(p => p.IngredientId == pi.IngredientId)).ToList());

    entity.Ingredients = validIngredients.Select(i => _mapper.Map<IngredientDto>(i).Id).ToList().OrderBy(i => i).ToList();

    return entity;
  }

  public async override Task<ServiceResult> GetAll() {
    ServiceResult result = new();
    try {
      var pIngredients = await _pIngredientRepository.GetAll();
      var ingredients = await _ingredientRepository.GetAll();

      var query = from plate in await _plateRepository.GetAll()
                  join pIngredient in pIngredients on plate.Id equals pIngredient.PlateId
                  select _mapper.Map<PlateDto>(plate, opt => opt.AfterMap((src, plt) => {
                    plt.Category = GetEnums.GetPlateCategory(plate.CategoryId);
                    plt.Ingredients = ingredients.Where(i => pIngredients.Any(pi => pi.PlateId == plate.Id && pi.IngredientId == i.Id)).Select(i => _mapper.Map<IngredientDto>(i)).ToList();
                  }));

      result.Data = query.Count() > 0 ? query.ToList() : null;

    } catch (Exception ex) {
      result.Success = false;
      result.Message = ex.Message;
    }
    return result;
  }

  public async override Task<PlateDto> GetById(int id) {

    var pIngredients = await _pIngredientRepository.GetAll();
    var ingredients = await _ingredientRepository.GetAll();

    var query = await _plateRepository.GetEntity(id).ContinueWith(plate => _mapper.Map<PlateDto>(plate.Result, opt => opt.AfterMap((src, plt) => {
      plt.Category = GetEnums.GetPlateCategory(plate.Result.CategoryId);
      plt.Ingredients = ingredients.Where(i => pIngredients.Any(pi => pi.PlateId == plate.Result.Id && pi.IngredientId == i.Id)).Select(i => _mapper.Map<IngredientDto>(i)).ToList();
    })));

    return query;
  }
}
