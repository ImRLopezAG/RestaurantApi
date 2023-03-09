using AutoMapper;
using Restaurant.Core.Application.Contracts.Core;
using Restaurant.Core.Application.Core.Models;
using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Application.Core;

public class GenericService<EntityVm, SaveEntityVm, Entity> : IGenericService<EntityVm, SaveEntityVm, Entity> where EntityVm : Base where SaveEntityVm : Base where Entity : BaseEntity {
  private readonly IGenericRepository<Entity> _repository;
  private readonly IMapper _mapper;

  public GenericService(IGenericRepository<Entity> repository, IMapper mapper) {
    _repository = repository;
    _mapper = mapper;
  }

  public virtual async Task<ServiceResult> GetAll() {
    ServiceResult result = new();
    try {
      var query = from entity in await _repository.GetAll()
                  select _mapper.Map<EntityVm>(entity);

      result.Data = query.Any() ? query : null;

    } catch (Exception ex) {
      result.Success = false;
      result.Message = ex.Message;
    }
    return result;
  }

  public virtual async Task<SaveEntityVm> GetEntity(int id) {
    var entity = await _repository.GetEntity(id);
    return _mapper.Map<SaveEntityVm>(entity);
  }

  public virtual async Task<ServiceResult> GetById(int id) {
    ServiceResult result = new();
    try {
      var entity = await _repository.GetEntity(id);
      result.Data = _mapper.Map<EntityVm>(entity);
    } catch (Exception ex) {
      result.Success = false;
      result.Message = ex.Message;
    }
    return result;
  }
  public virtual async Task<SaveEntityVm> Save(SaveEntityVm vm) {
    var entity = _mapper.Map<Entity>(vm);
    await _repository.Save(entity);
    return _mapper.Map<SaveEntityVm>(entity);
  }

  public virtual async Task<SaveEntityVm> Edit(SaveEntityVm vm) {
    var entity = _mapper.Map<Entity>(vm);
    await _repository.Update(entity);
    return _mapper.Map<SaveEntityVm>(entity);
  }

  public virtual async Task Delete(int id) {
    var entity = await _repository.GetEntity(id);
    await _repository.Delete(entity);
  }
}

