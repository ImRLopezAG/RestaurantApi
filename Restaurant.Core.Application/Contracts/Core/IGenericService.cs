using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Core.Models;

namespace Restaurant.Core.Application.Contracts.Core;
public interface IGenericService<EntityVm, SaveEntityVm, Entity> : IBaseService where EntityVm : class where SaveEntityVm : Base where Entity : class {
  Task<SaveEntityVm> Save(SaveEntityVm vm);
  Task<SaveEntityVm> Edit(SaveEntityVm vm);
  Task Delete(int id);
  Task<SaveEntityVm> GetEntity(int id);
}
