using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Core.Models;

namespace Restaurant.Core.Application.Core;

public interface IGenericController<TDto, TSaveDto, TEntity> where TDto : class where TSaveDto : Base where TEntity : class {
  Task<ActionResult<IEnumerable<TDto>>> List();
  Task<ActionResult<TDto>> GetById(int id);
  Task<ActionResult> Create([FromQuery] TSaveDto dto);
  Task<ActionResult<TDto>> Update([FromQuery] TSaveDto dto);
}
